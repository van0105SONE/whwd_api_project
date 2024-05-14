using Infrastructure.Model.Users;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.jwtService
{
    public class JwtService : IJwtService
    {
        private IConfiguration _Configuration { get; set; }
        public JwtService(IConfiguration configuration) { 
         _Configuration = configuration;
        }

        public string generateToken(ApplicationUser user)
        {
            try
            {
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_Configuration.GetSection("JwtConfig:secret").Value);

                var tokeDetails = new SecurityTokenDescriptor() {
                    Subject = new ClaimsIdentity(new[]
                    {
                      new Claim(ClaimTypes.Name, user.UserName),
                      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                      new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                       new Claim(JwtRegisteredClaimNames.Exp, DateTime.UtcNow.AddMinutes(2).ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(20),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                };

                var token = jwtTokenHandler.CreateToken(tokeDetails);
                return jwtTokenHandler.WriteToken(token);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public string generateRefreshToken()
        {
            try
            {

                var randomNumber = new byte[64];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(randomNumber);
                    return Convert.ToBase64String(randomNumber);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public ClaimsPrincipal GetPrincipleFromExpireToken(string token)
        {
            try
            {
                var symmetricKey = Encoding.UTF8.GetBytes(_Configuration.GetSection("JwtConfig:secret").Value);
                var tokenValidatorParameter = new TokenValidationParameters()
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey),
                    ValidateLifetime = false,
                };

                var jwtSecurityHandler = new JwtSecurityTokenHandler();
                SecurityToken? securityToken;
                var principal = jwtSecurityHandler.ValidateToken(token, tokenValidatorParameter, out securityToken);
                var jwtSecurityToken = securityToken as JwtSecurityToken;
                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");
                return principal;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
