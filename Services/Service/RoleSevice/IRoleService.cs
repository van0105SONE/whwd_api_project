using ApplicationCore.Dtos.Roles;
using ErrorOr;
using Infrastructure.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.RoleSevice
{
    public interface IRoleService
    {
        public ErrorOr<bool> addPosition(PositionTeamDto teamPosition, string userName);

        public ErrorOr<List<Position>> getPositions();
        public ErrorOr<List<ProjectTeam>> getProjectTeam();


        public ErrorOr<List<string>> getRoles();
        public Task<ErrorOr<bool>> addUserRole(UserRoleDto userRole);
    }
}
