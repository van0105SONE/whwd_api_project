using Infrastructure.DataBaseContext;
using Infrastructure.Model.Address;
using Infrastructure.Repository.Implement;
using Infrastructure.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.AddressService
{
    public class AddressService : IAddressService
    {
        private IAddressRepository _addressRepository;
        public AddressService(DatabaseContexts dbContext) { 
             _addressRepository = new AddressRepository(dbContext);
        }
        public bool createVillage(Village village)
        {
            try
            {
                bool isExist = _addressRepository.isVillageExist(village.villageName);
                if (!isExist)
                {
                    return false;
                }

               bool isSuccess =   _addressRepository.createVillage(village);
               return isSuccess;

            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<District> getDistricts()
        {
            try
            {
                List<District> districts = _addressRepository.getDistricts();    
                return districts;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Province> getProvinces()
        {
            try
            {
                List<Province> provinces =  _addressRepository.getProvinces();
                return provinces;
                 
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Village> getVillages()
        {
            try
            {
                List<Village> villages = _addressRepository.getVillages();  
                return villages;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Village> getVillagesByDistrict(string districtCode)
        {
            try
            {
                if (string.IsNullOrEmpty(districtCode))
                {
                    throw new Exception("Invalid District code, District code is required");
                }
                List<Village> villages = _addressRepository.getVillagesByDistrict(districtCode);
                return villages;
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
