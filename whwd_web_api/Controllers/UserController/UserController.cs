using AutoMapper;
using Infrastructure.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using whwd_web_api.Dtos;
using whwd_web_api.Dtos.UserDto;

namespace whwd_web_api.Controllers.UserController
{
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {

        private UserManager<ApplicationUser> _UserManager { get; set; }
        private IMapper _Mapping { get; set; }
        public UserController(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _UserManager = userManager;
            _Mapping = mapper;
        }


        [HttpPost]
        [Route("createUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userRequest)
        {
            try
            {
                var user = _Mapping.Map<ApplicationUser>(userRequest);
                var applicationUserName = await _UserManager.FindByNameAsync(userRequest.UserName);
                var applicationUserEmail = await _UserManager.FindByEmailAsync(userRequest.Email);

                if (applicationUserName == null)
                {
                    return Conflict(new MessageReponse()
                    {
                        isSuccess = false,
                        message = "Username is already exist"
                    });
                }
                else if (applicationUserEmail == null)
                {
                    return Conflict(new MessageReponse()
                    {
                        isSuccess = false,
                        message = "Username is already exist"
                    });
                }
                var result = await _UserManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    return CreatedAtAction(nameof(CreateUser), new MessageReponse() { isSuccess = true, message = "Successful created user" });
                }
                return CreatedAtAction(nameof(CreateUser), new MessageReponse() { isSuccess = true, message = "Successful Created" });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
