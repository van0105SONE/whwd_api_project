using ApplicationCore.Constanst;
using ApplicationCore.Dtos;
using ApplicationCore.Dtos.FunRaisingPlaceDto;
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

                if (user == null)
                {
                    return Error.Validation(ErrorCodes.Validation,"User invalid, require user data");
                }
                ApplicationUser? coordinator = await _userManager.FindByIdAsync(placeDto.coordinatorId);
                if (coordinator == null)
                {
                    return Error.Validation(ErrorCodes.Validation,"Coordinator invalid, require coordinato data");
                }
                Village village = _addressRepository.getVillageById(placeDto.Village.villageCode);

                if (village == null)
                {
                    return Error.Validation(ErrorCodes.Validation, "Village invalid, require village data");
                }

                if (placeDto.startDate >= DateTime.Now)
                {
                    return Error.Validation(ErrorCodes.Validation, "Start date must be greater or equal to date now");
                }

                FundRaisingPlace newPlace = _mapper.Map<FundRaisingPlace>(placeDto);

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

        public async Task<List<FundRaisingPlace>> getPlaceAll(BaseFilter filter)
        {
            try
            {
                PlaceFilter placeFilter = new PlaceFilter() {
                    page = filter.page,
                    pageSize  = filter.pageSize,
                    keywords = filter.keywords,
                };

                return await  _fundRaisingPlaceService.getPlaceAll(placeFilter);
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FundRaisingPlace> getPlaceById(Guid id)
        {
            try
            {
              return await  _fundRaisingPlaceService.getPlaceById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ErrorOr<MessageReponse<PlaceResponseDto>>> updatePlace(PlaceUpdateStatusDto placeDto)
        {
            try
            {
                var place =  await _fundRaisingPlaceService.getPlaceById(placeDto.Id);
                place.Status = placeDto.status;
                place.startDate = placeDto.startDate;
                place.endDate = placeDto.endDate;
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
                        message = "Fail to create, "
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<FundRaisingPlace>> getPlaceWithStatusConfirm(BaseFilter filter)
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

                return await _fundRaisingPlaceService.getPlaceAll(placeFilter);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
