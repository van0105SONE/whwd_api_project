using Infrastructure.Model.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.IRepository
{
    public interface IAddressRepository
    {
        public bool createVillage(Village village);

        public bool isVillageExist(string name);
        public List<Village> getVillages();
        public List<Village> getVillagesByDistrict(string districtCode);

        public List<District> getDistricts();
        public District getDistrictById(String disctrictCode);
        public List<Province> getProvinces();

    }
}
