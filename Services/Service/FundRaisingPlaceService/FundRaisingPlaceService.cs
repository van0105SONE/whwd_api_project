using ApplicationCore.Constanst;
using ApplicationCore.Dtos;
using ApplicationCore.Dtos.FunRaisingPlaceDto;
using ApplicationCore.Dtos.UserDto;
using ApplicationCore.Filter;
using AutoMapper;
using ErrorOr;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Address;
using Infrastructure.Model.Place;
using Infrastructure.Model.Users;
using Infrastructure.Repository.FundRaisingPlaceRepos;
using Infrastructure.Repository.Implement;
using Infrastructure.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.FundRaisingPlaceService
{
    public class FundRaisingPlaceService : IFundRaisingPlaceService
    {
        private IMapper _mapper { get; set; }
        private readonly UserManager<ApplicationUser> _userManager;
        private IFundRaisingPlaceRepos _fundRaisingPlaceService { get; set; }
        private IAddressRepository _addressRepository { get; set; }
        public FundRaisingPlaceService(UserManager<ApplicationUser> userManager, DatabaseContexts context, IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
            _addressRepository = new AddressRepository(context);
            _fundRaisingPlaceService = new FundRaisingPlaceRepos(context);
            
        }
        public async Task<ErrorOr<MessageReponse<PlaceResponseDto>>> createPlace(FundRaisingPlaceDto placeDto)
        {
            try
            {
                ApplicationUser? user =  await _userManager.FindByIdAsync(placeDto.userId);
                FundRaisingPlace newPlace = _mapper.Map<FundRaisingPlace>(placeDto);
                if (user == null)
                {
                    return Error.Validation(ErrorCodes.Validation,"User invalid, require user data");
                }
                ApplicationUser? coordinator = await _userManager.FindByIdAsync(placeDto.coordinatorId);
                if (coordinator == null)
                {
                    return Error.Validation(ErrorCodes.Validation,"Coordinator invalid, require coordinato data");
                }

                Village village = _addressRepository.getVillageById(placeDto.village.villageCode);

                if (village == null)
                {
                    Village newVillage = createVillageWithCodeNull(placeDto.village.district.districtCode, placeDto.village.villageName);
                    village = newVillage;
                }


                 newPlace.Status  = Constant.COORDINATE_STATUSES[0];
                 newPlace.CreateBy = user;
                 newPlace.CoordinateBy = coordinator;
                 newPlace.Village = village;
                 var result =  await _fundRaisingPlaceService.createPlace(newPlace);

                if (result)
                {
                    PlaceResponseDto response =   _mapper.Map<PlaceResponseDto>(newPlace);
                    return new MessageReponse<PlaceResponseDto>()
                    {
                        isSuccess = true,
                        statusCode = 200,
                        message = "Successful",
                        data = response
                    };
                }
                else
                {
                    return new MessageReponse<PlaceResponseDto>()
                    {
                        isSuccess = false,
                        statusCode = 500,
                        message = "Fail to create fund raising place"
                    };
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<MessageReponse<List<PlaceResponseDto>>> getPlaceAll(BaseFilter filter)
        {
            try
            {
                PlaceFilter placeFilter = new PlaceFilter() {
                    page = filter.page,
                    pageSize  = filter.pageSize,
                    keywords = filter.keywords,
                };

                var result = await  _fundRaisingPlaceService.getPlaceAll(placeFilter);
                var response =    _mapper.Map<List<PlaceResponseDto>>(result);
                return new MessageReponse<List<PlaceResponseDto>>
                {
                    statusCode = 200,
                    isSuccess = true,
                    message = "Successful",
                    data = response
                };
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<MessageReponse<PlaceResponseDto>> getPlaceById(Guid id)
        {
            try
            {
              var result = await  _fundRaisingPlaceService.getPlaceById(id);
              var response =  _mapper.Map<PlaceResponseDto>(result);
                return new MessageReponse<PlaceResponseDto>
                {
                    isSuccess = true,
                    statusCode = 200,
                    message = "Successful",
                    data = response
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ErrorOr<MessageReponse<PlaceResponseDto>>> updatePlace(Guid Id,PlaceUpdateStatusDto placeDto)
        {
            try
            {
                var place = await _fundRaisingPlaceService.getPlaceById(Id);
                var user = await _userManager.FindByIdAsync(placeDto.userId);
                if (place == null)
                {
                    return Error.Validation(ErrorCodes.Validation, "Place is not found in the system");
                }
                else if (user == null)
                {
                    return Error.Validation(ErrorCodes.Validation, "User is not found in the system");
                }


                place.Status = placeDto.status;
                if (placeDto.status == "CONFIRM")
                {
                    place.startDate = placeDto.startDate;
                    place.endDate = placeDto.endDate;
                }

                place.UpdateBy = user;

                if (place.startDate.Value.Date >= DateTime.Now.Date)
                {
                    return new MessageReponse<PlaceResponseDto>()
                    {
                        isSuccess = false,
                        statusCode = 400,
                        message = "Start date must greater than current date"
                    };
                }


                var result = await _fundRaisingPlaceService.updatePlace(place);
                if (result)
                {
                    var response = _mapper.Map<PlaceResponseDto>(place);
                    return new MessageReponse<PlaceResponseDto>()
                    {
                        isSuccess = true,
                        statusCode = 200,
                        message = "Successful",
                        data = response
                    };
                }
                else
                {
                    return new MessageReponse<PlaceResponseDto>()
                    {
                        isSuccess = false,
                        statusCode = 400,
                        message = "Fail to update place status"
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<MessageReponse<List<PlaceResponseDto>>> getPlaceWithStatusConfirm(BaseFilter filter)
        {
            try
            {
                PlaceFilter placeFilter = new PlaceFilter()
                {
                    page = filter.page,
                    pageSize = filter.pageSize,
                    keywords = filter.keywords,
                    status = Constant.COORDINATE_STATUSES[3]
                };

                var result = await _fundRaisingPlaceService.getPlaceAll(placeFilter);
                var response =  _mapper.Map<List<PlaceResponseDto>>(result);
                return new MessageReponse<List<PlaceResponseDto>>()
                {
                    statusCode = 200,
                    isSuccess = true,
                    message = "Successful",
                    data = response
                };
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private Village createVillageWithCodeNull(string districtCode, string villageName)
        {
            try
            {
                var district = _addressRepository.getDistrictById(districtCode);
                Village village = new Village()
                {
                    villageCode = Guid.NewGuid().ToString(),
                    villageName = villageName,
                    district = district,
                };
                var villageResult = _addressRepository.createVillage(village);
                return village;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
