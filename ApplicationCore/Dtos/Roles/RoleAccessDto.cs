using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dtos.Roles
{
    public class RoleAccessDto
    {
        
        public Guid   Id { get; set; }
        public string accessName { get; set; }
        public bool isActive { get; set; }
    }
}
