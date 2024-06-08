using Infrastructure.DataBaseContext;
using Infrastructure.Model.Address;
using Microsoft.AspNetCore.Mvc;
using Services.Service.AddressService;

namespace whwd_web_api.Controllers.AddressController
{
    [ApiController]
    public class AddressController : Controller
    {

        private DatabaseContexts _dbContext { get; set; }
        private IAddressService _addressService { get; set; }
        public AddressController(DatabaseContexts context)
        {
            _dbContext = context;
            _addressService = new AddressService(context);
        }

        [HttpGet]
        [Route("getProvinces")]

        public IActionResult getProvinces()
        {
            try
            {
                List<Province> provinces = _addressService.getProvinces();
                return Ok(provinces);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("getDistricts")]
        public IActionResult getDistricts()
        {
            try
            {
                List<District> districts = _addressService.getDistricts();
                return Ok(districts);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("getVillages")]
        public IActionResult getVillages()
        {
            try
            {
                List<Village> villages = _addressService.getVillages();
                return Ok(villages);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
