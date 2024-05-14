using Infrastructure.Model.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.AddressService
{
    public interface IAddressService
    {
        public bool createVillage(Village village);
        public List<Village> getVillages();
        public List<Village> getVillagesByDistrict(string districtCode);

        public List<District> getDistricts();
        public List<Province> getProvinces();
    }
}
