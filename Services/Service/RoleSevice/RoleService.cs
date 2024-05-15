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
        private readonly RoleManager<string> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleService(UserManager<ApplicationUser> userManager, DatabaseContexts dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleRepository = new RoleRepository(dbContext);
        }
        public RoleService(RoleManager<String> roleManager,UserManager<ApplicationUser> userManager,DatabaseContexts dbContext, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
            _roleRepository = new RoleRepository(dbContext);
        }

        public ErrorOr<bool> addPosition(PositionTeamDto teamPosition,  string userName)
        {
            try
            { 


                  if (teamPosition == null)
                   {

                      return Error.Validation(code: "ValidationError", description: "Invalid data can't be null");
                   }else if (string.IsNullOrEmpty(userName))
                   {

                      return Error.Validation(code: "ValidationError", description: "Invalid user information can't be null");
                   }else if (teamPosition.teamId == null || teamPosition.teamId == Guid.Empty)
                   {

                      return Error.Validation(code: "ValidationError", description: "Invalid team can't be null");
                   }else if(teamPosition.positionId == null || teamPosition.teamId == Guid.Empty)
                   {

                      return Error.Validation(code: "ValidationError", description: "Position Id isn't valid");
                   }
                   Position position = _roleRepository.getPositionById(teamPosition.positionId);
                   ProjectTeam team = _roleRepository.getTeamById(teamPosition.teamId);
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
    }
}
