using ApplicationCore.Constanst;
using ApplicationCore.Dtos;
using ApplicationCore.Dtos.FunRaisingPlaceDto;
using ApplicationCore.Dtos.UserDto;
using ApplicationCore.Filter;
using AutoMapper;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Address;
using Infrastructure.Model.Place;
using Infrastructure.Model.University;
using Infrastructure.Model.Users;
using Infrastructure.Repository.Implement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Service.FundRaisingPlaceService;
using whwd_web_api.Errors;

namespace whwd_web_api.Controllers.WorkController
{
    [ApiController]
    public class FundRaisingPlaceController : Controller
    {
        
        private UserManager<ApplicationUser> _userManager { get; set; }
        private IFundRaisingPlaceService _fundRaisingService { get; set; }
        private IMapper _mapper { get; set; }
        private DatabaseContexts _databaseContest { get; set; }
         public FundRaisingPlaceController(UserManager<ApplicationUser> userManager,DatabaseContexts contexts, IMapper mapper) { 
            _userManager = userManager;
            _fundRaisingService = new FundRaisingPlaceService(userManager, contexts, mapper);
            _databaseContest = contexts;
            _mapper = mapper;
         }

        [HttpPost]
        [Route("createPlace")]
        public async  Task<ActionResult> createPlace([FromBody] FundRaisingPlaceDto placeDto)
        {
            try
            {
                var result =  await _fundRaisingService.createPlace(placeDto);
                if (result.IsError)
                {
                    return  Ok(ErrorHandler<PlaceResponseDto>.HandleErrorResponse(result.FirstError.Code, result.FirstError.Description));
                } else {
                    return Ok(result.Value);
                }       
            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("updatePlace/{id}")]
        public async Task<ActionResult> updatePlaceData(Guid id,[FromBody] FundRaisingPlaceDto placeDto)
        {
            try
            {
                var place = _databaseContest.fundRaisingPlaces.FirstOrDefault(t => t.Id == id);

                place.googleMapLink = placeDto.googleMapLink;
                place.PhoneNumber = place.PhoneNumber;
                place.startDate = placeDto.startDate;
                place.endDate = placeDto.endDate;
                place.Email = placeDto.email;

                _databaseContest.fundRaisingPlaces.Update(place);
                _databaseContest.SaveChanges();

                if (place == null)
                {
                    return BadRequest(new MessageReponse<PlaceResponseDto>()
                    {
                        statusCode = 400,
                        message = "Place Id is invalid",
                        isSuccess = false,
                    });
                }

                Village? village = _databaseContest.villages.FirstOrDefault(t => t.villageCode == placeDto.village.villageCode);
                var department = _databaseContest.districts.FirstOrDefault(t => t.districtCode == placeDto.village.district.districtCode);

                if (village == null)
                {
                    Village newVillage = new Village() { 
                        villageCode = Guid.NewGuid().ToString(),
                        villageName = placeDto.village.villageName
                     };
                    _databaseContest.villages.Add(village);
                    _databaseContest.SaveChanges();
                   village = newVillage;
                }

                var coordinator = _databaseContest.Users.FirstOrDefault(u => u.Id == placeDto.coordinatorId.ToString());
                place.CoordinateBy = coordinator;
                place.Village = village;
                _databaseContest.fundRaisingPlaces.Update(place);
                _databaseContest.SaveChanges();

                return Ok(
                    new MessageReponse<PlaceResponseDto>() {
                        statusCode = 201,
                        message = "Successful",
                        isSuccess = true,
                        });

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deletePlace/{Id}")]
        public async Task<ActionResult> deletePlace(Guid Id)
        {
            try
            {
              var placer =  _databaseContest.fundRaisingPlaces.FirstOrDefault(t => t.Id == Id);
                if (placer == null)
                {
                    return BadRequest(
                        new  MessageReponse<PlaceResponseDto>(){
                            statusCode = 400,
                           isSuccess = false,
                           message = "Fail to remove place"
                              }
                        );
                }
                _databaseContest.fundRaisingPlaces.Remove(placer);
                _databaseContest.SaveChanges();

                return Ok(
     new MessageReponse<PlaceResponseDto>()
     {
         statusCode = 201,
         isSuccess = true,
         message = "Successful",
         data = _mapper.Map<PlaceResponseDto>(placer)
     }
     );
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpPatch]
        [Route("updateStatus/{Id}")]
        public async Task<ActionResult> UpdateStatus(Guid Id,[FromBody] PlaceUpdateStatusDto placeStatusDto)
        {
            try
            {
                    var result = await _fundRaisingService.updatePlace(Id,placeStatusDto);
                    if (result.IsError)
                    {
                        return Ok(ErrorHandler<FundRaisingPlaceDto>.HandleErrorResponse(result.FirstError.Code, result.FirstError.Description));
                    }
                    else
                    {
                        return Ok(result.Value);
                    }
                }
                catch (Exception ex)
                {
                    return Problem(ex.Message);
                }
  
        }



        [HttpGet]
        [Route("getPlaceAll")]
        public async Task<IActionResult> GetPlaceAll([FromQuery] BaseFilter filter)
        {
            try
            {
                var result = await _fundRaisingService.getPlaceAll(filter);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("getPlaceWithStatusConfirm")]
        public async Task<IActionResult> GetPlaceWithStatusConfirm([FromQuery] BaseFilter filter)
        {
            try
            {
              return   Ok(await _fundRaisingService.getPlaceWithStatusConfirm(filter));
            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("getPlaceTypes")]
        public async Task<IActionResult> GetPlaceTypes()
        {
            try
            {
                return Ok(Constant.COORDINATE_STATUSES);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
