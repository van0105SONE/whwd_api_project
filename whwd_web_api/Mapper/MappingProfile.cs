using AutoMapper;
using Infrastructure.Model;
using Infrastructure.Model.Volunteer;
using Infrastructure.Model.Work;
using whwd_web_api.Dtos;
using whwd_web_api.Dtos.UserDto;

namespace whwd_web_api.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            //Mapping request to Model
            CreateMap<UserDto, ApplicationUser>();
            CreateMap<DepartmentDto, Department>();
            CreateMap<GenerationDto, Generation>();

            //Mapping model to response
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<Generation,  GenerationDto>();
        }
    }
}
