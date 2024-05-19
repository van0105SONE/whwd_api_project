using ApplicationCore.Dtos.AuthenticationDto;
using Infrastructure.DataBaseContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Service.jwtService;

namespace whwd_web_api.Controllers
{
    [ApiController]
    public class TokenController : Controller
    {

        private DatabaseContexts _DbContext;
        private  IJwtService _JwtServie;
         public TokenController(DatabaseContexts DbContext, IConfiguration Configuration) { 
             this._DbContext = DbContext;
            this._JwtServie = new JwtService(Configuration);
         }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenApiModel tokenApiModel)
        {
            if (tokenApiModel == null) {
                return BadRequest("Invalid client request");
            }

            string accessToken = tokenApiModel.accessToken;
            string refreshToken = tokenApiModel.refreshToken; 

            var principal = _JwtServie.GetPrincipleFromExpireToken(accessToken);
            var userName = principal.Identity.Name;

            var user = _DbContext.Users.SingleOrDefault(u => u.UserName == userName);   
            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiry <= DateTime.Now)
            {
                return BadRequest("Invalid client request");
            }

            var newAccessToken = _JwtServie.generateToken(user);
            var newRefreshToken = _JwtServie.generateRefreshToken(); 

            user.RefreshToken = newRefreshToken;
            _DbContext.SaveChanges();

            return Ok(new AuthenticatedResponse() { 
                RefreshToken = newRefreshToken, Token = newAccessToken });
        }

        [HttpPost, Authorize]
        [Route("Revoke")]
        public IActionResult Revoke()
        {
            var userName = User.Identity.Name;
            var user = _DbContext.Users.SingleOrDefault(t => t.UserName == userName);
            if (user == null)
            {
                return BadRequest();
            }
            user.RefreshToken = null;
            _DbContext.SaveChanges();

            return NoContent();

        }
    }
}
