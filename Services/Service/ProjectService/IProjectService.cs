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

        public Task<ErrorOr<MessageReponse<ProjectPlanResponseDto>>> createProject(ProjectPlanDto projectPlan);
        public Task<ErrorOr<MessageReponse<ProjectPlanResponseDto>>> deleteProjectPlan(Guid Id);
        public Task<ErrorOr<MessageReponse<List<DonateThingResponseDto>>>> createDonateThing(List<DonateThingDto> donateThingDto);
        public Task<ErrorOr<MessageReponse<DonateThingResponseDto>>> updateDonateThing(Guid Id,DonateThingDto donateThingDto);
        Task<ErrorOr<MessageReponse<DonateThingResponseDto>>> deleteDonateThing(Guid Id);
        Task<ErrorOr<MessageReponse<List<SchoolResponseDto>>>> createSchool(List<SchoolDto> schoolDto);
        Task<ErrorOr<MessageReponse<List<SchoolResponseDto>>>> getSchools();
        Task<ErrorOr<MessageReponse<List<DonateThingResponseDto>>>> getDonateThings(DonateThingFilter filter);
        Task<ErrorOr<MessageReponse< List<ProjectPlanResponseDto>>>> getProjects(BaseFilter filter);
        Task<ErrorOr<MessageReponse<ProjectPlanResponseDto>>> getProjectPlanById(Guid Id);
        Task<ErrorOr<ProjectPlan>> getActiveProjectPlan();
    }
}
