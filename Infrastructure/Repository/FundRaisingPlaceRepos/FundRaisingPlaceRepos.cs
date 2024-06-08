using ApplicationCore.Constanst;
using ApplicationCore.Filter;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Place;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.FundRaisingPlaceRepos
{
    public class FundRaisingPlaceRepos : IFundRaisingPlaceRepos
    {
        private DatabaseContexts _dbContext { get; set; }
        public FundRaisingPlaceRepos(DatabaseContexts context) {
           _dbContext = context; 
        }
        public async Task<bool> createPlace(FundRaisingPlace placeParam)
        {
            try
            {
               await  _dbContext.fundRaisingPlaces.AddAsync(placeParam);
               await  _dbContext.SaveChangesAsync();
                return true;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> deletePlace(Guid Id)
        {
            try
            {
                var place = await _dbContext.fundRaisingPlaces.FirstAsync(t => t.Id == Id);
                _dbContext.Remove(place);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<FundRaisingPlace>> getPlaceAll(PlaceFilter filter)
        {
            try
            {
                List<FundRaisingPlace> places = new List<FundRaisingPlace>();
                if (string.IsNullOrEmpty(filter.status))
                {
                    places = await _dbContext.fundRaisingPlaces.Where(t => t.Status != Constant.COORDINATE_STATUSES[3]).Skip((filter.page - 1) * filter.pageSize).Take(filter.pageSize).ToListAsync();
                }
                else
                {
                    places = await _dbContext.fundRaisingPlaces.Where(t => t.Status == Constant.COORDINATE_STATUSES[3]).Skip((filter.page - 1) * filter.pageSize).Take(filter.pageSize).ToListAsync();
                }

                return places;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FundRaisingPlace> getPlaceById(Guid id)
        {
            try
            {
                var place = await _dbContext.fundRaisingPlaces.FirstAsync(t => t.Id == id);
                return place;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> updatePlace(FundRaisingPlace placeParam)
        {
            try
            {
                _dbContext.fundRaisingPlaces.Update(placeParam);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
