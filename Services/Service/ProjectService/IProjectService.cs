using ApplicationCore.Dtos.RecipientDto;
using ApplicationCore.Dtos.Work;
using ApplicationCore.Filter;
using ErrorOr;
using Infrastructure.Model.Work;


namespace Services.Service.PositionService
{
    public interface IProjectService
    {

        Task<ErrorOr<bool>> createProject(ProjectPlanDto projectPlan);
        Task<ErrorOr<bool>> deleteProjectPlan(Guid Id);
        Task<ErrorOr<bool>> createDonateThing(DonateThingDto donateThingDto);
        Task<ErrorOr<bool>> updateDonateThing(UpdateDonateThingDto donateThingDto);
        Task<ErrorOr<bool>> deleteDonateThing(Guid Id);


        Task<ErrorOr<bool>> createSchool(SchoolDto schoolDto);
        Task<ErrorOr<List<DonateThing>>> getDonateThings(DonateThingFilter filter);
        Task<ErrorOr<List<ProjectPlan>>> getProjects(BaseFilter filter);
        Task<ErrorOr<ProjectPlan>> getProjectPlanById();
        Task<ErrorOr<ProjectPlan>> getActiveProjectPlan();
    }
}
