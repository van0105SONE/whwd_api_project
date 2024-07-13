using ApplicationCore.Constanst;
using ApplicationCore.Dtos.Roles;
using AutoMapper;
using ErrorOr;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Users;
using Infrastructure.Repository.RoleRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using Services.Middleware;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.RoleSevice
{
    public class RoleService : IRoleService
    {
        IMapper _mapper;
        private readonly IRoleRepository _roleRepository;
        private readonly RoleManager<ApplicationRoles> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleService(UserManager<ApplicationUser> userManager, DatabaseContexts dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleRepository = new RoleRepository(dbContext);
        }

        public RoleService(RoleManager<ApplicationRoles> roleManager,UserManager<ApplicationUser> userManager,DatabaseContexts dbContext, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
            _roleRepository = new RoleRepository(dbContext);
        }

        public ErrorOr<List<Position>> getPositions()
        {
            try
            {
                return _roleRepository.getPositions();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ErrorOr<List<ProjectTeam>> getProjectTeam()
        {
            try
            {
                return _roleRepository.getTeams();
            }catch(Exception ex)
            {
               throw new Exception(ex.Message);
            }
        }

        public ErrorOr<List<ApplicationRoles>> getUserRoles()
        {
            try
            {
                List<ApplicationRoles> roles = _roleRepository.getRoles() ;
                return roles;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


		public async Task<ErrorOr<bool>> checkUserRole(CheckRole userRole)
		{
            try
            {
                if (string.IsNullOrEmpty(userRole.userId))
                {
                    return Error.Validation("User id is required");
                }else if (string.IsNullOrEmpty( userRole.pageName))
                {
                    return Error.Validation("Page nam is required");
                }

                    ApplicationUser user = await _userManager.FindByIdAsync(userRole.userId);
                    List<PositionTeam> positionTeams = _roleRepository.getPositionTeamByUserId(userRole.userId);
                    bool isInRole =  await    _userManager.IsInRoleAsync(user, userRole.actionName);
                    if (!isInRole)
                    {
                    return false;
                    }
				    bool inTeam = positionTeams.Any(t => t.Team.Name == userRole.pageName);
				    if (!inTeam)
                    {
                    return false;
                    }

                    return true;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
		}


        public async Task<ErrorOr<AccessRight>> createRoleAccess(RoleAccessDto roleParams)
        {
            try { 

             ApplicationRoles role = _roleRepository.getRoleById(roleParams.Id);
            if (role != null)
            {
                  AccessRight accessRight = new AccessRight()
                  {
                    Id = Guid.NewGuid(),
                    Name = roleParams.accessName,
                    Roles = role
                };


             return   _roleRepository.createRoleAccess(accessRight);
            }
            else
            {
                    throw new Exception("Role isn't exist");
            }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ErrorOr<List<AccessRight>>> getRoleAccesses(Guid roleId)
        {
            try
            {
              return   _roleRepository.GetRoleAccess(roleId);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ErrorOr<ApplicationRoles>> createRole(string roleName)
        {
            try
            {
                ApplicationRoles role = new ApplicationRoles() {
                   Name = roleName,
                   NormalizedName = roleName.ToUpper()
                };

                return _roleRepository.createRole(role);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
