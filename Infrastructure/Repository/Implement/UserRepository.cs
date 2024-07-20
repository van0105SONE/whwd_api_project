

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

        public List<ApplicationUser> getUsers(BaseFilter filter)
        {
            try
            {
                List<ApplicationUser> users = _dbContext.Users.Skip((filter.page - 1) * filter.pageSize).Include(t => t.BornVillage).ThenInclude(t => t.district).ThenInclude(t => t.province).Include(t => t.CurrentVillage).ThenInclude(t => t.district).ThenInclude(t => t.province).Include(t => t.Major).Include(t => t.projectTeam).Include(t => t.position).Include(t => t.Role).ToList();
                return users;
            }catch(Exception ex) { 
                throw new Exception(ex.Message);    
            }
        }

        public ApplicationUser getUserById(string Id)
        {
            try
            {
                ApplicationUser? user = _dbContext.Users.Include(t => t.BornVillage).ThenInclude(t => t.district).ThenInclude(t => t.province).Include(t => t.CurrentVillage).ThenInclude(t => t.district).ThenInclude(t => t.province).Include(t => t.Major).Include(t => t.projectTeam).Include(t => t.position).Include(t => t.Role).FirstOrDefault(t => t.Id  == Id);
                return user;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
