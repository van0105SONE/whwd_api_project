using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.Users
{
    public class AccessRight
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActice { get; set; }
        public ApplicationRoles Roles { get; set; } 
        public AccessRight() { }
    }
}
