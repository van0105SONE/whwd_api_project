using ApplicationCore.Filter;
using ErrorOr;
using Infrastructure.Model.Work;


namespace Infrastructure.Repository.ProjectRepository
{
    public interface IProjectPlanRepository
    {
        Task<ErrorOr<bool>> create(ProjectPlan projectParam);
        void delete(Guid Id);
        Task<ErrorOr<ProjectPlan>> getProjectActiveProject();
        Task<ErrorOr<bool>> createDonateThing(DonateThing projectParam);
        Task<ErrorOr<bool>> updateDonateThing(DonateThing donateThingParams);
        Task<ErrorOr<bool>> deleteDonateThing(Guid Id);
        Task<ErrorOr<List<DonateThing>>> GetDonateThings(DonateThingFilter filter);
        Task<ErrorOr<DonateThing>> GetDonateThingById(Guid Id);

    }
}
