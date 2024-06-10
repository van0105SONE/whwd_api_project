using Infrastructure.DataBaseContext;
using Infrastructure.Model.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public bool addRole()
        {
            try
            {
                return true;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool createRole()
        {
            try
            {
                return true;
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

        public List<string> getRoles()
        {
            try
            {
                List<string> roles =  _dbContexts.Roles.Select(t => t.Name).ToList();
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
	}
}
