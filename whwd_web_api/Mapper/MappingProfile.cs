using AutoMapper;
using Infrastructure.Model;
using Infrastructure.Model.Volunteer;
using Infrastructure.Model.Work;
using whwd_web_api.Dtos;
using whwd_web_api.Dtos.UserDto;
using whwd_web_api.Dtos.Work;

namespace whwd_web_api.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            //Mapping request to Model
            CreateMap<UserDto, ApplicationUser>();
            CreateMap<DepartmentDto, Department>();
            CreateMap<GenerationDto, Generation>();


            //project model
            CreateMap<ProjectPlanDto, ProjectPlan>();
            CreateMap<ProjectPlan, ProjectPlanDto>();

            //Mapping model to response
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<Generation,  GenerationDto>();
        }
    }
}
