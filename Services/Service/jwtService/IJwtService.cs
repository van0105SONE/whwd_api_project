using Infrastructure.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.jwtService
{
    public interface IJwtService
    {
         public  string generateToken(ApplicationUser user);
         public string generateRefreshToken();

        public ClaimsPrincipal GetPrincipleFromExpireToken(string token);


    }
}
