using ApplicationCore.Dtos;
using ApplicationCore.Dtos.UserDto;
using ApplicationCore.Filter;
using AutoMapper;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.Service.AddressService;
using Services.Service.UserService;
using whwd_web_api.Errors;


namespace whwd_web_api.Controllers.UserController
{
    [ApiController]

    public class UserController : Controller
    {
        private UserManager<ApplicationUser> _UserManager { get; set; }
        private IMapper _Mapping { get; set; }
        private IUserService _userService { get; set; }
        private IAddressService _addressService { get; set; }

        public UserController(UserManager<ApplicationUser> userManager, DatabaseContexts dbContext, IMapper mapper)
        {
            _UserManager = userManager;
            _userService = new UserService(userManager, dbContext, mapper);
            _addressService = new AddressService(dbContext);
            _Mapping = mapper;
        }


        [HttpPost]
        [Route("createUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            try
            {
                var userResult = await _userService.createUser(userDto);

                if (userResult.IsError)
                {
                    return   Ok(ErrorHandler<ApplicationUser>.HandleErrorResponse(userResult.FirstError.Code, userResult.FirstError.Description));
                }else
                {
                    return Ok(userResult.Value);
                }

            }
            catch (Exception ex)
            {
                return Ok(new MessageReponse<ApplicationUser>() { 
                      statusCode = 500,
                      isSuccess = false,
                      message = ex.Message
                });;

            }
        }
        
        [HttpPut]
        [Route("updateUser/{userId}")]
        public  async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UserDto userDto){
           try{
                var updateResult = await _userService.updateUser(userId.ToString(), userDto);
                if (updateResult.IsError)
                {
                    return Ok(ErrorHandler<ApplicationUser>.HandleErrorResponse(updateResult.FirstError.Code, updateResult.FirstError.Description));
                }
                else
                {
                    return Ok(updateResult.Value);
                }

           }catch(Exception ex){
                return Ok(new MessageReponse<ApplicationUser>()
                {
                    statusCode = 500,
                    isSuccess = false,
                    message = ex.Message
                }); 
            }
        }

        [HttpGet]
        [Route("getUsers")]
        public async Task<IActionResult> GetUsers([FromQuery] BaseFilter filter)
        {
            try
            {
                List<UserReponseDto> users = _userService.GetUsers(filter);
                var jsonString = JsonConvert.SerializeObject(users, Formatting.Indented);
                return Ok(jsonString);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("getUserById")]
		public async Task<IActionResult> GetUserById([FromQuery] String Id)
		{
			try
			{
				ApplicationUser user = await _userService.getUserById(Id);
                UserReponseDto userReponse =   _Mapping.Map<UserReponseDto>(user);
				var jsonString = JsonConvert.SerializeObject(userReponse, Formatting.Indented,
						new JsonSerializerSettings()
						{
							ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
						}
					);


				return Ok(jsonString);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}
    }
}
