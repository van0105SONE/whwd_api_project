using ApplicationCore.Dtos;
using ApplicationCore.Dtos.AuthenticationDto;
using AutoMapper;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Service.jwtService;


namespace whwd_web_api.Controllers
{
    [ApiController]
    public class AuthController : Controller
    {
        private UserManager<ApplicationUser> _UserManager { get; set; }
        private IMapper  _mapping { get; set; }
        private IJwtService _JwtService { get; set; }
        private DatabaseContexts _DbContexts { get; set; }


        public AuthController(IMapper mapping,UserManager<ApplicationUser> userManager,DatabaseContexts context, IConfiguration configiuration)
        {
            _UserManager = userManager;
            _mapping = mapping;
            _JwtService = new JwtService(configiuration);
            _DbContexts = context;

        }

        [HttpPost]
        [Route("LogIn")]
        public async Task<IActionResult> LogIn([FromBody] LogInDto userRequest)
        {
            try
            {
               ApplicationUser? user =  await _UserManager.FindByNameAsync(userRequest.UserName);
               if (user == null) {
                    return NotFound("Username isn't found");
               }


                bool isExist = await  _UserManager.CheckPasswordAsync(user, userRequest.Password);
 
               if (isExist)
                {
                    String refreshToken = _JwtService.generateRefreshToken();
                    String token = _JwtService.generateToken(user);
                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
                    _DbContexts.SaveChanges();
                    return Ok(new AuthenticatedResponse()
                    {
                     Token  =  token,
                     RefreshToken = refreshToken,
                    });
                }
                else
                {
                    return Unauthorized("Password isn't correct");
                }

            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpPost]
        [Route("resetPassword")]

        public async Task<IActionResult> ResetPassword([FromBody] PasswordResetDto request)
        {
            try
            {
                ApplicationUser? user = await _UserManager.FindByNameAsync(request.UserName);
                if (user == null)
                {
                    return NotFound("Username isn't exist");
                }

               var result = await _UserManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPasssword);

                if (result.Succeeded)
                {
                    return Ok(new MessageReponse() { isSuccess = true, message = "Succesful changed password"});
                }
                else
                {
                    return Unauthorized("Current password isn't correct");
                }
            }catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
