using ApplicationCore.Filter;
using ErrorOr;
using Infrastructure.Model.Place;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.FundRaisingPlaceRepos
{
    public interface IFundRaisingPlaceRepos
    {
        public Task<bool> createPlace(FundRaisingPlace placeParam);

        public Task<bool> updatePlace(FundRaisingPlace placeParam);

        public Task<List<FundRaisingPlace>> getPlaceAll(PlaceFilter filter);

        public Task<FundRaisingPlace> getPlaceById(Guid id);


        public Task<bool> deletePlace(Guid Id);
    }
}
