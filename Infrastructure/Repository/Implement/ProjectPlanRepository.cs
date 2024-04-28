using Infrastructure.DataBaseContext;
using Infrastructure.Model.Work;
using Infrastructure.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.Implement
{
    public class ProjectPlanRepository : IProjectPlanRepository
    {
        private DatabaseContexts _DbContext { get; set; }
        public ProjectPlanRepository(DatabaseContexts dbContext)
        {
            _DbContext = dbContext;
        }

        public void create(ProjectPlan projectParam)
        {
            try
            {
                _DbContext.projectPlan.Add(projectParam);
                _DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        async public void update(ProjectPlan projectParam)
        {
            try
            {
                ProjectPlan? projectPlan = await _DbContext.projectPlan.FirstOrDefaultAsync(t => t.Id == projectParam.Id);
                if (projectPlan == null)
                {
                    throw new Exception("Project plan isn't found");
                }

                projectPlan.ProjectName = projectParam.ProjectName;
                projectPlan.Description = projectParam.Description;
                projectPlan.DonateThing = projectParam.DonateThing;
                projectPlan.UpdateBy = projectParam.UpdateBy;
                projectPlan.TargetValue = projectParam.TargetValue;

                _DbContext.projectPlan.Update(projectPlan);
                _DbContext.SaveChanges(true);
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
             ProjectPlan? projectPlan = await   _DbContext.projectPlan.FirstOrDefaultAsync(t => t.Id == Id);
             if (projectPlan == null)
                {
                    throw new Exception("Project isn't found");
                }
                _DbContext.projectPlan.Remove(projectPlan);
                _DbContext.SaveChanges(true);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
