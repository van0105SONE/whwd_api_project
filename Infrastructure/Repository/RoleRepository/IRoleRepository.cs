using Infrastructure.Model.Users;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.RoleRepository
{
    public interface IRoleRepository
    {
        public ApplicationRoles createRole(ApplicationRoles role);
        public List<ApplicationRoles> getRoles();
        public ApplicationRoles getRoleById(Guid Id);
        public List<Position> getPositions();
        public List<ProjectTeam> getTeams();
        public Position getPositionById(Guid Id);
        public ProjectTeam getTeamById(Guid Id);
        public AccessRight createRoleAccess(AccessRight roleAccess);
        public List<AccessRight> GetRoleAccess(Guid Id);
        public AccessRight getAccessRightById(Guid Id);


    }
}
