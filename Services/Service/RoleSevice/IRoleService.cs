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
        public Task<ErrorOr<bool>> addPosition(Guid teamId, Guid positionId, string userName);

        public ErrorOr<List<Position>> getPositions();
        public ErrorOr<List<ProjectTeam>> getProjectTeam();
    }
}
