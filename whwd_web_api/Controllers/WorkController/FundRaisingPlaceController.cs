using ApplicationCore.Constanst;
using ApplicationCore.Dtos.FunRaisingPlaceDto;
using ApplicationCore.Filter;
using AutoMapper;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Place;
using Infrastructure.Model.Users;
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
         public FundRaisingPlaceController(UserManager<ApplicationUser> userManager,DatabaseContexts contexts, IMapper mapper) { 
            _userManager = userManager;
            _fundRaisingService = new FundRaisingPlaceService(userManager, contexts, mapper);
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
        [Route("updateStatus")]
        public async Task<ActionResult> UpdateStatus(Guid Id,[FromQuery] PlaceUpdateStatusDto placeStatusDto)
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
