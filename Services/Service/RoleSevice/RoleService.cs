using ApplicationCore.Dtos.Roles;
using AutoMapper;
using ErrorOr;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Users;
using Infrastructure.Repository.RoleRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleService(UserManager<ApplicationUser> userManager, DatabaseContexts dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleRepository = new RoleRepository(dbContext);
        }
        public RoleService(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager,DatabaseContexts dbContext, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
            _roleRepository = new RoleRepository(dbContext);
        }

        public async Task<ErrorOr<bool>> addPosition(Guid teamId, Guid positionId,  string userName)
        {
            try
            { 
                 if (teamId == Guid.Empty)
                   {

                      return Error.Validation(code: "ValidationError", description: "Invalid user information can't be null");
                   }else if ( positionId == Guid.Empty)
                   {

                      return Error.Validation(code: "ValidationError", description: "Invalid team can't be null");
                   }
                   Position position = _roleRepository.getPositionById(positionId);
                   ProjectTeam team = _roleRepository.getTeamById(teamId);
                   if (team == null)
                   {
                    return Error.Validation("NotFound", "Can't find the team on the system");
                   }else if(position == null)
                   {
                    return Error.Validation("NotFound", "Can't find the position on the system");
                   }




                   ApplicationUser? user = _userManager.FindByNameAsync(userName).Result;     
                   PositionTeam positonTeam = new PositionTeam()
                   {
                        Id = Guid.NewGuid(),
                        Position = position,
                        Team = team,
                        User = user
                   };
                  
                   bool isSuccess =  _roleRepository.addPosition(positonTeam);
                  return isSuccess;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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

        public ErrorOr<List<string>> getRoles()
        {
            try
            {
                List<string> roles = _roleRepository.getRoles() ;
                return roles;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ErrorOr<bool>> addUserRole(UserRoleDto userRole)
        {
            try
            {
                 var userResult =  await _userManager.FindByIdAsync(userRole.userId);
                if (userResult == null)
                {
                    Error.Validation("NotFound", "User can't found");
                }

                var result = await _userManager.AddToRoleAsync(userResult, userRole.role);
                if (result.Succeeded)
                {
                   return true;
                }else
                {
                   return Error.Failure("Failure", "Something wend wrong");
                }

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
