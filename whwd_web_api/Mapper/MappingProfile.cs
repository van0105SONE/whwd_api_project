using AutoMapper;
using Infrastructure.Model.Address;
using Infrastructure.Model.University;
using Infrastructure.Model.Users;
using Infrastructure.Model.Work;
using whwd_web_api.Dtos;
using whwd_web_api.Dtos.Address;
using whwd_web_api.Dtos.University;
using whwd_web_api.Dtos.UserDto;
using whwd_web_api.Dtos.Work;

namespace whwd_web_api.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            //Mapping request to Model
            CreateMap<UserDto, ApplicationUser>();
            CreateMap<MajorDto, Major>();
            CreateMap<VillageDto, Village>();
            CreateMap<DistrictDto, District>();

            CreateMap<DepartmentDto, Department>();
            CreateMap<FacultyDto, Faculty>();
            CreateMap<UniversityDto, University>();
            //project model
            CreateMap<ProjectPlanDto, ProjectPlan>();
            CreateMap<ProjectPlan, ProjectPlanDto>();

            //Mapping model to response
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<Department, DepartmentDto>();
        }
    }
}
