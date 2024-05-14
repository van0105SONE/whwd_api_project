using AutoMapper;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Address;
using Infrastructure.Model.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Service.AddressService;
using Services.Service.jwtService;

namespace whwd_web_api.Controllers.AddressController
{
    public class AddressController : Controller
    {

        private DatabaseContexts _dbContext { get; set; }
        private IAddressService _addressService { get; set; }
        public AddressController(DatabaseContexts context)
        {
            _dbContext = context;
            _addressService = new AddressService(context);
        }

        public IActionResult GetProvinces()
        {
            try
            {
               List<Province> provinces = _addressService.getProvinces();
                return Ok(provinces);
            }catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        public IActionResult GetDistricts()
        {
            try
            {
             List<District> districts =   _addressService.getDistricts();
             return Ok(districts);
            }catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        public IActionResult GetVillages()
        {
            try
            {
                List<Village> villages = _addressService.getVillages();
                return Ok(villages);
            }catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        public IActionResult GetVillageByDistrict([FromQuery] string districtCode)
        {
            try
            {
                List<Village> villages = _addressService.getVillagesByDistrict(districtCode);
                return Ok(villages);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        
    }
}
