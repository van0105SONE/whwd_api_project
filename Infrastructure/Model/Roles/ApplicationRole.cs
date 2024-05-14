using Infrastructure.Model.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.Roles
{
    public class ApplicationRole : IdentityRole
    {
         public PositionTeam PositionTeam { get; set; }
    }
}
