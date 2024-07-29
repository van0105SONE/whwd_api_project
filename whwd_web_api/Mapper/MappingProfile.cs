using ApplicationCore.Dtos.Accounts;
using ApplicationCore.Dtos.Address;
using ApplicationCore.Dtos.ConjointDto;
using ApplicationCore.Dtos.Dashboard;
using ApplicationCore.Dtos.Donate;
using ApplicationCore.Dtos.FunRaisingPlaceDto;
using ApplicationCore.Dtos.Recipient;
using ApplicationCore.Dtos.RecipientDto;
using ApplicationCore.Dtos.Reports;
using ApplicationCore.Dtos.RoleDto;
using ApplicationCore.Dtos.Roles;
using ApplicationCore.Dtos.StudentDto;
using ApplicationCore.Dtos.TransactionDto;
using ApplicationCore.Dtos.University;
using ApplicationCore.Dtos.UserDto;
using ApplicationCore.Dtos.Work;
using AutoMapper;
using Infrastructure.Model.Account;
using Infrastructure.Model.Address;
using Infrastructure.Model.Donate;
using Infrastructure.Model.Place;
using Infrastructure.Model.Recipient;
using Infrastructure.Model.reports;
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
            CreateMap<ApplicationRoles, RoleDto>();

            CreateMap<Transaction, TransactionResponseDto>();
            CreateMap<TransactionDto, Transaction>();

            CreateMap<Infrastructure.Model.reports.RecipientReport, RecipientBySchool>();
            CreateMap<Department, DepartmentDto>();
			CreateMap<ProjectTeam, ProjectTeamResponse>();
			CreateMap<Position, PositionResponse>();
			CreateMap<Major, MajorDto > ();
			CreateMap<Village, VillageReponseDto>();
			CreateMap<District, DistrictResponseDto>();
            CreateMap<Province, ProvinceResponseDto>();
            CreateMap<Recipient, RecipientReponseDto>();
            CreateMap<Major, MajorResponseDto>();
            CreateMap<Department, DepartmentResponseDto>();
            CreateMap<Faculty, FacultyResponseDto>();
            CreateMap<University, UniversityResponseDto>();
            CreateMap<FundRaisingPlace, PlaceResponseDto>();



            CreateMap<AccountDto, Account>();
            CreateMap<DonationDto, Donation>();
            CreateMap<Donation, DonationResponseDto>();
            CreateMap<Donator, DonatorResponseDto>();

            CreateMap<FundRaisingPlaceDto, FundRaisingPlace>();
            CreateMap<DepartmentDto, Department>();
            CreateMap<FacultyDto, Faculty>();
            CreateMap<UniversityDto, University>();

            //project model
            CreateMap<ProjectPlanDto, ProjectPlan>();
            CreateMap<ProjectPlan, ProjectPlanResponseDto>();
            CreateMap<AccountDto, Account>();
            CreateMap<Account, AccountResponseDto>();

            CreateMap<DonateThingDto, DonateThing>();
			CreateMap<DonateThing, DonateThingResponseDto>();

			CreateMap<SchoolDto, School>();
			CreateMap<School, SchoolResponseDto>();
            CreateMap<SchoolResponseDto, SchoolSerieResponseDto>();
            //Mapper 
            CreateMap<ConjointDto, Conjoint>();

            // map user role
            CreateMap<ApplicationRoles, RoleResponse>();
            CreateMap<AccessRight, AccessRightResponse>();
        }
    }
}
