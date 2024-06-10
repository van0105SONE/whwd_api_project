using ApplicationCore.Dtos.ConjointDto;
using ApplicationCore.Dtos.Donate;
using ApplicationCore.Dtos.FunRaisingPlaceDto;
using ApplicationCore.Dtos.RecipientDto;
using ApplicationCore.Dtos.Roles;
using ApplicationCore.Dtos.StudentDto;
using ApplicationCore.Dtos.University;
using ApplicationCore.Dtos.UserDto;
using ApplicationCore.Dtos.Work;
using AutoMapper;
using Infrastructure.Model.Account;
using Infrastructure.Model.Address;
using Infrastructure.Model.Donate;
using Infrastructure.Model.Place;
using Infrastructure.Model.Recipient;
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
            AllowNullCollections = true;

            //Mapping request to Model
            CreateMap<RecipientDto, Recipient>();
            CreateMap<UserDto, ApplicationUser>();
            CreateMap<MajorDto, Major>();
            CreateMap<VillageDto, Village>();
            CreateMap<DistrictDto, District>();

			//Mapping model to response
			CreateMap<ApplicationUser, UserReponseDto>();
			CreateMap<Department, DepartmentDto>();
            CreateMap<PositionTeam, PositiontTeamResponseDto>();
			CreateMap<ProjectTeam, ProjectTeamResponse>();
			CreateMap<Position, PositionResponse>();
			CreateMap<PositionTeam, PositionTeamDto>();
			CreateMap<Major, MajorDto > ();
			CreateMap<Village, VillageDto>();
			CreateMap<District, DistrictDto>();
            CreateMap <UserType, UserTypeDto>();

			CreateMap<AccountDto, Account>();
            CreateMap<DonationDto, Donation>();

            CreateMap<FundRaisingPlaceDto, FundRaisingPlace>();

            CreateMap<DepartmentDto, Department>();
            CreateMap<FacultyDto, Faculty>();
            CreateMap<UniversityDto, University>();

            //project model
            CreateMap<ProjectPlanDto, ProjectPlan>();
            CreateMap<ProjectPlan, ProjectPlanResponseDto>();

			CreateMap<DonateThingDto, DonateThing>();
			CreateMap<DonateThing, DonateThingResponseDto>();

			CreateMap<SchoolDto, School>();
			CreateMap<School, SchoolResponseDto>();






            //Mapper 
            CreateMap<ConjointDto, Conjoint>();
        }
    }
}
