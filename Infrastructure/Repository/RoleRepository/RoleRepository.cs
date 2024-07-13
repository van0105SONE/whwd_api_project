using Infrastructure.DataBaseContext;
using Infrastructure.Model.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.RoleRepository
{
    public class RoleRepository : IRoleRepository
    {
        private DatabaseContexts _dbContexts;

        public RoleRepository(DatabaseContexts dbContext) {
          _dbContexts = dbContext;
        }

        public bool addPosition(PositionTeam position)
        {
            try
            {
                _dbContexts.position_teams.Add(position);
                _dbContexts.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public ApplicationRoles createRole(ApplicationRoles role)
        {
            try
            {
                _dbContexts.Roles.Add(role);
                _dbContexts.SaveChanges();
                return role;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<PositionTeam> getPositionTeams()
        {
            try
            {
                List<PositionTeam> positions = _dbContexts.position_teams.ToList();
                return positions;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Position> getPositions()
        {
            try
            {
                  return _dbContexts.positions.ToList();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public Position getPositionById(Guid Id)
        {
            try
            {
                Position? position =   _dbContexts.position.FirstOrDefault(t => t.Id == Id);
                return position;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ProjectTeam> getTeams()
        {
            try
            {
                 return _dbContexts.project_teams.ToList();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public ProjectTeam getTeamById(Guid Id)
        {
            try
            {
                ProjectTeam? projectTeam = _dbContexts.project_teams.FirstOrDefault(t => t.Id == Id);
                return projectTeam;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ApplicationRoles> getRoles()
        {
            try
            {
                List<ApplicationRoles> roles =  _dbContexts.Roles.Include(t => t.accessRights).ToList();
                return roles;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

		public List<PositionTeam> getPositionTeamByUserId(string userId)
		{
            try
            {
                return _dbContexts.position_teams.Include( t=> t.Position).Include(t => t.Team).Where(t => t.User.Id == userId).ToList();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
		}


        public AccessRight getAccessRightById(Guid Id)
        {
            try
            {
               return  _dbContexts.accessRight.FirstOrDefault(t => t.Id == Id);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public AccessRight getAccessById(Guid Id)
        {
            try
            {
                return _dbContexts.accessRight.FirstOrDefault(t => t.Id == Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public  AccessRight createRoleAccess(AccessRight roleAccess)
        {
            try
            {

                _dbContexts.accessRight.Add(roleAccess);
                _dbContexts.SaveChanges();
                return roleAccess;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ApplicationRoles getRoleById(Guid Id)
        {
            try
            {
               return _dbContexts.Roles.FirstOrDefault(t => t.Id == Id.ToString());
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        List<AccessRight> IRoleRepository.GetRoleAccess(Guid Id)
        {
            try
            {
               return _dbContexts.accessRight.ToList();
            }catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}
