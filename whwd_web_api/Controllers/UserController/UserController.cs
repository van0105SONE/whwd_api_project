using ApplicationCore.Dtos;
using ApplicationCore.Dtos.UserDto;
using AutoMapper;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Address;
using Infrastructure.Model.University;
using Infrastructure.Model.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Service.AddressService;
using Services.Service.UniversityService;
using Services.Service.UserService;


namespace whwd_web_api.Controllers.UserController
{
    [ApiController]
    
    public class UserController : Controller
    {

        private UserManager<ApplicationUser> _UserManager { get; set; }
        private IMapper _Mapping { get; set; }
        private IUserService _userService { get; set; }
        private IAddressService _addressService { get; set; }
        private IUniversityService _universityService { get; set; }
        public UserController(UserManager<ApplicationUser> userManager,DatabaseContexts dbContext ,IMapper mapper)
        {
            _UserManager = userManager;
            _userService = new UserService(userManager, dbContext, mapper);
            _addressService = new AddressService(dbContext);
            _universityService = new UniversityService(dbContext);
            _Mapping = mapper;
        }


        [HttpPost]
        [Route("createUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            try
            {
                var userResult = await _userService.createUser(userDto);
                return userResult.Match(t => CreatedAtAction(nameof(CreateUser), new MessageReponse() { isSuccess = true, message = "Successful Created" }), err => Problem(err.FirstOrDefault().Description) );
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpGet]
        [Route("getProvinces")]

        public IActionResult getProvinces()
        {
            try
            {
              List<Province> provinces =  _addressService.getProvinces();
                return Ok(provinces);
            }catch (Exception ex)
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

        [HttpGet]
        [Route("getUnivesities")]
        public IActionResult getUniversity()
        {
            try
            {
                List<University> villages = _universityService.getUniversities();
                return Ok(villages);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpGet]
        [Route("getFaculties")]
        public IActionResult getFaculties()
        {
            try
            {
                List<Faculty> villages = _universityService.getFaculties();
                return Ok(villages);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("getDepartments")]
        public IActionResult getDepartments()
        {
            try
            {
                List<Department> villages = _universityService.getDepartment();
                return Ok(villages);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpGet]
        [Route("getUserTypes")]
        public IActionResult getUserTypes()
        {
            try
            {
                List<UserType> userTypes = _userService.getUserTypes();
                return Ok(userTypes);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
