using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.Users
{
    public class ApplicationRoles : IdentityRole
    {
      
        public ICollection<AccessRight> accessRights { get; set; }  
    }
}
