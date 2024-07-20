using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dtos.RoleDto
{
    public class RoleResponse
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public List<AccessRightResponse> accessRights { get; set; }
    }
}
