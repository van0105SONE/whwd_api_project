
using ApplicationCore.Filter;
using ErrorOr;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Recipient;
using Infrastructure.Model.Work;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.ProjectRepository
{
    public class ProjectPlanRepository : IProjectPlanRepository
    {
        private DatabaseContexts _DbContext { get; set; }
     
        public ProjectPlanRepository(DatabaseContexts dbContext)
        {
            _DbContext = dbContext;
        }

        public async Task<ErrorOr<bool>> create(ProjectPlan projectParam)
        {
            try
            {
                _DbContext.projectPlan.Add(projectParam);
                _DbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async void delete(Guid Id)
        {
            try
            {
                ProjectPlan? projectPlan = await _DbContext.projectPlan.FirstOrDefaultAsync(t => t.Id == Id);
                if (projectPlan == null)
                {
                    throw new Exception("Project isn't found");
                }
                _DbContext.projectPlan.Remove(projectPlan);
                _DbContext.SaveChanges(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ErrorOr<bool>> createDonateThing(DonateThing projectParam)
        {
            try
            {
                _DbContext.donateThings.Add(projectParam);
                _DbContext.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ErrorOr<ProjectPlan>> getProjectActiveProject()
        {
            try
            {
                ProjectPlan projectPlan = await _DbContext.projectPlan.OrderByDescending(t => t.CreateAt.Date).FirstAsync();
                return projectPlan;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ErrorOr<List<DonateThing>>> GetDonateThings(DonateThingFilter filter)
        {
            try
            {
                int skip  = ((filter.page - 1) * filter.pageSize);
                List<DonateThing>  donateThings =  _DbContext.donateThings.Skip(skip).Take(filter.pageSize).Where(t => t.ProjectPlan.Id == filter.projectId).ToList();
                return donateThings;
            }catch(Exception ex) { 
               throw new Exception(ex.Message); 
            }
        }

        public async Task<ErrorOr<bool>> updateDonateThing(DonateThing donateThingParams)
        {
            try
            {
                _DbContext.donateThings.Update(donateThingParams);
                _DbContext.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ErrorOr<bool>> deleteDonateThing(Guid Id)
        {
            try
            {
                DonateThing? donateThings = _DbContext.donateThings.FirstOrDefault(t => t.Id == Id);
                _DbContext.donateThings.Remove(donateThings);
                return true;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ErrorOr<DonateThing>> GetDonateThingById(Guid Id)
        {
            try
            {
              DonateThing? donateThing =  _DbContext.donateThings.FirstOrDefault(t => t.Id == Id);
              return donateThing;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ErrorOr<bool>> createSchool(School schoolParam)
        {
            try
            {
                _DbContext.schoools.Add(schoolParam);
                _DbContext.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

		public async Task<List<ProjectPlan>> getProjects(BaseFilter filter)
		{
            try
            {
              return await  _DbContext.projectPlan.Include(t => t.donateThings).Include(t => t.schools).Skip((filter.page - 1) * filter.pageSize).Take(filter.pageSize).ToListAsync() ;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
		}
	}
}
