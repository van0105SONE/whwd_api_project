using ApplicationCore.Dtos.Roles;
using ErrorOr;
using Infrastructure.Model.Users;
using Services.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.RoleSevice
{
    public interface IRoleService
    {
        public Task<ErrorOr<ApplicationRoles>> createRole(string roleName);
        public ErrorOr<List<Position>> getPositions();
        public ErrorOr<List<ProjectTeam>> getProjectTeam();
        public Task<ErrorOr<bool>> checkUserRole(CheckRole userRole);
        public ErrorOr<List<ApplicationRoles>> getUserRoles();
        public Task<ErrorOr<List<AccessRight>>> getRoleAccesses(Guid roleId);
        public  Task<ErrorOr<AccessRight>> createRoleAccess(RoleAccessDto roleAccessParams);



    }
}
