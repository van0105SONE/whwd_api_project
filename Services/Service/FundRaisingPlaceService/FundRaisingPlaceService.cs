using ApplicationCore.Constanst;
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
using System.Linq;
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
        public async Task<ErrorOr<bool>> createPlace(FundRaisingPlaceDto placeDto)
        {
            try
            {
                ApplicationUser? user =  await _userManager.FindByIdAsync(placeDto.userId);

                if (user == null)
                {
                    return Error.NotFound("User invalid, require user data");
                }
                ApplicationUser? coordinator = await _userManager.FindByIdAsync(placeDto.coordinatorId);
                if (coordinator == null)
                {
                    return Error.NotFound("Coordinator invalid, require coordinato data");
                }
                Village village = _addressRepository.getVillageById(placeDto.villageCode);

                if (village == null)
                {
                    return Error.NotFound("Village invalid, require village data");
                }
                FundRaisingPlace newPlace = _mapper.Map<FundRaisingPlace>(placeDto);
                newPlace.Status  = Constant.COORDINATE_STATUSES[0];
                newPlace.CreateBy = user;
                newPlace.CoordinateBy = coordinator;
                newPlace.Village = village;
                var result =  await _fundRaisingPlaceService.createPlace(newPlace);
                return result;
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

       async public Task<ErrorOr<bool>> updatePlace(PlaceUpdateStatusDto placeDto)
        {
            try
            {
                var place =  await _fundRaisingPlaceService.getPlaceById(placeDto.Id);
                place.Status = placeDto.status;
                var result = await _fundRaisingPlaceService.updatePlace(place);
                return result;
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
