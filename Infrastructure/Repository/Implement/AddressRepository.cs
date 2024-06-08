using Infrastructure.DataBaseContext;
using Infrastructure.Model.Address;
using Infrastructure.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


namespace Infrastructure.Repository.Implement
{
    public class AddressRepository : IAddressRepository
    {
        private readonly DatabaseContexts _dbContext;
        public AddressRepository(DatabaseContexts dbContext)
        {
            _dbContext = dbContext;
        }
        public bool createVillage(Village village)
        {
            try
            {
                _dbContext.villages.Add(village);
                _dbContext.SaveChanges();
                return true;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public District getDistrictById(string disctrictCode)
        {
            try
            {
              District? district =  _dbContext.districts.FirstOrDefault(t => t.districtCode == disctrictCode);
                return district;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<District> getDistricts()
        {
            try
            {
                List<District> districts =  _dbContext.districts.Include(t => t.province).ToList();
                return districts;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Province> getProvinces()
        {
            try
            {
               List<Province> provinces =   _dbContext.provinces.ToList();
                return provinces;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Village getVillageById(string villageCode)
        {
            try
            {
                Village? villages = _dbContext.villages.FirstOrDefault(t => t.villageCode == villageCode);
                return villages;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Village> getVillages()
        {
            try
            {
                List<Village> villages = _dbContext.villages.Include(t => t.district).ThenInclude(t => t.province).ToList();
                return villages;
            }catch (Exception ex) { 
              throw new Exception(ex.Message);
            }
        }

        public List<Village> getVillagesByDistrict(string districtCode)
        {
            try
            {
                List<Village> villages = _dbContext.villages.Where(t => t.district.districtCode == districtCode).Include(t => t.district).ThenInclude(t => t.province).ToList();
                return villages;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool isVillageExist(string name)
        {
            try
            {
               bool isExist = _dbContext.villages.Any(t => t.villageName.ToLower().Equals(name.ToLower()));
                return isExist;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
