﻿using ApplicationCore.Dtos.Roles;
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
        public Task<ErrorOr<bool>> addPosition(Guid teamId, Guid positionId, string userName);

        public ErrorOr<List<Position>> getPositions();
        public ErrorOr<List<ProjectTeam>> getProjectTeam();

        public Task<ErrorOr<bool>> checkUserRole(CheckRole userRole);
        public ErrorOr<List<string>> getRoles();
        public Task<ErrorOr<bool>> addUserRole(UserRoleDto userRole);
    }
}
