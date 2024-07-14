using ApplicationCore.Dtos;
using ApplicationCore.Dtos.RecipientDto;
using ApplicationCore.Dtos.Work;
using ApplicationCore.Filter;
using ErrorOr;
using Infrastructure.Model.Work;


namespace Services.Service.PositionService
{
    public interface IProjectService
    {

        public Task<ErrorOr<bool>> createProject(ProjectPlanDto projectPlan);
       public Task<ErrorOr<bool>> deleteProjectPlan(Guid Id);
        public Task<ErrorOr<MessageReponse<List<DonateThingResponseDto>>>> createDonateThing(List<DonateThingDto> donateThingDto);
        public Task<ErrorOr<MessageReponse<DonateThingResponseDto>>> updateDonateThing(UpdateDonateThingDto donateThingDto);
        Task<ErrorOr<bool>> deleteDonateThing(Guid Id);


        Task<ErrorOr<MessageReponse<List<SchoolResponseDto>>>> createSchool(List<SchoolDto> schoolDto);
        Task<ErrorOr<MessageReponse<List<SchoolResponseDto>>>> getSchools();
        Task<ErrorOr<List<DonateThing>>> getDonateThings(DonateThingFilter filter);
        Task<ErrorOr<List<ProjectPlan>>> getProjects(BaseFilter filter);
        Task<ErrorOr<ProjectPlan>> getProjectPlanById();
        Task<ErrorOr<ProjectPlan>> getActiveProjectPlan();
    }
}
