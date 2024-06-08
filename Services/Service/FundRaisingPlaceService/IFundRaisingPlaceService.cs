using ApplicationCore.Dtos.FunRaisingPlaceDto;
using ApplicationCore.Filter;
using ErrorOr;
using Infrastructure.Model.Place;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.FundRaisingPlaceService
{
    public interface IFundRaisingPlaceService
    {
        public Task<ErrorOr<bool>> createPlace(FundRaisingPlaceDto placeDto);

        public Task<ErrorOr<bool>> updatePlace(PlaceUpdateStatusDto placeDto);

        public Task<List<FundRaisingPlace>> getPlaceAll(BaseFilter filter);

        public Task<List<FundRaisingPlace>> getPlaceWithStatusConfirm(BaseFilter filter);

        public Task<FundRaisingPlace> getPlaceById(Guid id);
    }
}
