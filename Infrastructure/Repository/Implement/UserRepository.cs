

using ApplicationCore.Filter;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Users;
using Infrastructure.Repository.IRepository;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Implement
{
    public class UserRepository : IUserRepository
    {

        private DatabaseContexts _dbContext { get; set; }
        public  UserRepository(DatabaseContexts dbContext) { 
            _dbContext = dbContext;
        }

        public  bool addPosition(Position position)
        {
            try
            {
               _dbContext.positions.Add(position);
               return true;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool addTeam(ProjectTeam teamParams)
        {
            try
            {
                _dbContext.project_teams.Add(teamParams);   
                return true;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool deletePosition(Guid Id)
        {
            try
            {
                Position position = _dbContext.positions.First(x => x.Id == Id);
                _dbContext.positions.Remove(position);
                return true;
            }catch (Exception ex) { 
               throw new Exception(ex.Message);
            }

        }

        public bool deleteTeam(Guid Id)
        {
            try
            {
                ProjectTeam team = _dbContext.project_teams.First(t => t.Id == Id);
                _dbContext.project_teams.Remove(team);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Position getPositionById(Guid Id)
        {
            try
            {
                Position position = _dbContext.positions.First(t => t.Id == Id);
                return position;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Position> getPositions()
        {
            try
            {
                List<Position> position = _dbContext.positions.ToList();
                return position;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ProjectTeam getTeamById(Guid Id)
        {
            try
            {
                ProjectTeam team = _dbContext.project_teams.First(t => t.Id == Id);
                return team;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ProjectTeam> GetTeams()
        {
            try
            {
                List<ProjectTeam> teams = _dbContext.project_teams.ToList();
                return teams;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<UserType> getUserTypes()
        {
            try
            {
              List<UserType> userTypes =  _dbContext.userTypes.ToList();
             return userTypes;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UserType getUserTypeById(string Id)
        {
            try
            {
                UserType userTypes = _dbContext.userTypes.FirstOrDefault(t => t.Id == Id.ToString());
                return userTypes;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ApplicationUser> getUsers(BaseFilter filter)
        {
            try
            {
                List<ApplicationUser> users = _dbContext.Users.Skip((filter.page - 1) * filter.pageSize).ToList();
                return users;
            }catch(Exception ex) { 
                throw new Exception(ex.Message);    
            }
        }

        public ApplicationUser getUserById(string Id)
        {
            try
            {
                ApplicationUser? user = _dbContext.Users.Include(t => t.BornVillage).ThenInclude(t => t.district).Include(t => t.UserType).Include(t => t.CurrentVillage).ThenInclude(t => t.district).Include(t => t.Major).Include(t => t.positionTeams).ThenInclude(t => t.Team).Include(t => t.positionTeams).ThenInclude(t => t.Position).FirstOrDefault(t => t.Id  == Id);
                return user;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
