using ApplicationCore.Dtos.StudentDto;
using ApplicationCore.Dtos.University;
using ApplicationCore.Dtos.UserDto;
using ApplicationCore.Dtos.Work;
using AutoMapper;
using Infrastructure.Model.Account;
using Infrastructure.Model.Address;
using Infrastructure.Model.Student;
using Infrastructure.Model.University;
using Infrastructure.Model.Users;
using Infrastructure.Model.Work;
using whwd_web_api.Dtos.Accounts;
using whwd_web_api.Dtos.Address;

namespace whwd_web_api.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            //Mapping request to Model
            CreateMap<StudentDto, Student>();
            CreateMap<UserDto, ApplicationUser>();
            CreateMap<MajorDto, Major>();
            CreateMap<VillageDto, Village>();
            CreateMap<DistrictDto, District>();
            CreateMap<DonateThingDto, DonateThing>();
            CreateMap<AccountDto, Account>();

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
