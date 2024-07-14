using ApplicationCore.Constanst;
using ApplicationCore.Dtos;
using ApplicationCore.Dtos.RecipientDto;
using ApplicationCore.Dtos.Work;
using ApplicationCore.Filter;
using AutoMapper;
using ErrorOr;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Address;
using Infrastructure.Model.Recipient;
using Infrastructure.Model.Users;
using Infrastructure.Model.Work;
using Infrastructure.Repository.Implement;
using Infrastructure.Repository.IRepository;
using Infrastructure.Repository.ProjectRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.VisualBasic;
using Services.Middleware;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Error = ErrorOr.Error;

namespace Services.Service.PositionService
{
    public class ProjectService : IProjectService
    {
        IMapper _mapper { get; set; }

        IProjectPlanRepository _projectRepository { get; set; }
        IAddressRepository _addressRepository { get; set; }
        UserManager<ApplicationUser> _userManager { get; set; }
        
        CheckUserRoles roleMiddleWare { get; set; }
        public ProjectService(UserManager<ApplicationUser> userManager, DatabaseContexts context, IMapper mapper) {
            _mapper = mapper;
            _userManager = userManager;
            _projectRepository = new ProjectPlanRepository(context);
            roleMiddleWare = new CheckUserRoles(context, userManager);
            _addressRepository = new AddressRepository(context);
        }

        public async Task<ErrorOr<bool>> createProject(ProjectPlanDto projectPlanParam)
        {
            try
            {

                ProjectPlan projectPlan  =  _mapper.Map<ProjectPlan>(projectPlanParam);
                ApplicationUser user = await _userManager.FindByIdAsync(projectPlanParam.userId);

                
                  if (user == null)
                  {
                    return Error.Validation(ErrorCodes.Validation, "User ist found on system"); 
                  }else if (projectPlanParam.StartDate <= projectPlanParam.EndDate.Date)
                  {
                    return Error.Validation(ErrorCodes.Validation, "End date must be greater than start date");
                  }
                  var result  = await  _projectRepository.closeCurrentPlan();

                  if (!result.Value)
                  {
                     return Error.Validation(ErrorCodes.Validation, "End date must be greater than start date");
                  }
                 
                    projectPlan.IsActive = true;
                    projectPlan.valueInKip = 0;
                    projectPlan.ValueInBath = 0;
                    projectPlan.ValueInBath = 0;
                    projectPlan.CreateBy = user;
                    return await _projectRepository.create(projectPlan);
   

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<ErrorOr<bool>> deleteProjectPlan(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<ErrorOr<ProjectPlan>> getActiveProjectPlan()
        {
            throw new NotImplementedException();
        }



        public Task<ErrorOr<ProjectPlan>> getProjectPlanById()
        {
            throw new NotImplementedException();
        }

        public async Task<ErrorOr<MessageReponse<List<DonateThingResponseDto>>>> createDonateThing(List<DonateThingDto> donateThingDtos)
        {
            try
            {
                List<DonateThing> donateThingList = new List<DonateThing>();
                bool isSuccess = false;
                foreach(var donateThingDto in donateThingDtos)
                {
                    DonateThing donateThing = _mapper.Map<DonateThing>(donateThingDto);
                    donateThing.totalPrice = donateThing.Unit * donateThingDto.Price;
                    ApplicationUser? user = await _userManager.FindByIdAsync(donateThingDto.userId);

                    var projectResult = await _projectRepository.getProjectActiveProject();
                    if (projectResult.Value == null)
                    {
                        return Error.Validation(ErrorCodes.Validation, "project is not found");
                    }
                    else if (user == null)
                    {
                        return Error.Validation(ErrorCodes.Validation, "User is not found");
                    }

                    donateThing.CreateBy = user;
                    donateThing.ProjectPlan = projectResult.Value;
                    var result = await _projectRepository.createDonateThing(donateThing);
                    isSuccess = result.Value;
                    donateThingList.Add(donateThing);
                }

                if (!isSuccess)
                {
                    return new MessageReponse<List<DonateThingResponseDto>>()
                    {
                        isSuccess = true,
                        statusCode = 500,
                        message = "Fail to create donate thing, due to something went wrong"
                    };
                }
                else
                {
                    List<DonateThingResponseDto> donateThingReponse = _mapper.Map<List<DonateThingResponseDto>>(donateThingList);
                    return new MessageReponse<List<DonateThingResponseDto>>() {
                        isSuccess = true,
                        statusCode = 200,
                        message = "Successful",
                        data = donateThingReponse
                    };
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<ErrorOr<List<DonateThing>>> getDonateThings(DonateThingFilter filter)
        {
            try
            {
              return  _projectRepository.GetDonateThings(filter);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       public async Task<ErrorOr<MessageReponse<DonateThingResponseDto>>>  updateDonateThing(UpdateDonateThingDto donateThingDto)
        {
            try
            {
                 var donateThingResult = await _projectRepository.GetDonateThingById(donateThingDto.Id);
                if (donateThingResult.IsError)
                {
                    return Error.Failure("Failure", "Something went wrong");
                }
                else if (donateThingResult.Value == null)
                {
                    return Error.NotFound("NotFound", "Can't find donate things");
                }
                DonateThing donateThing = donateThingResult.Value;
                
                donateThing.UpdateBy = await _userManager.FindByIdAsync(donateThingDto.userId);
                donateThing.Name = donateThingDto.Name;
                donateThing.Price = donateThingDto.Price;
                donateThing.Unit = donateThingDto.Unit;
                donateThing.UnitType = donateThingDto.UnitType;
                donateThing.totalPrice = donateThing.Price * donateThing.Unit; 
                var result =  await _projectRepository.updateDonateThing(donateThing);
                if (result.IsError)
                {
                    return new MessageReponse<DonateThingResponseDto>()
                    {
                        isSuccess = true,
                        statusCode = 500,
                        message = "Fail to create donate thing, due to something went wrong"
                    };
                }
                else
                {
                    DonateThingResponseDto donateThingReponse = _mapper.Map<DonateThingResponseDto>(donateThingDto);
                    return new MessageReponse<DonateThingResponseDto>()
                    {
                        isSuccess = true,
                        statusCode = 200,
                        message = "Successful",
                        data = donateThingReponse
                    };
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<ErrorOr<bool>> deleteDonateThing(Guid Id)
        {
            try
            {
               return _projectRepository.deleteDonateThing(Id);
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

      public async  Task<ErrorOr<MessageReponse<List<SchoolResponseDto>>>> createSchool(List<SchoolDto> schoolDtos)
        {
            try
            {
                bool isSuccess = false;
                List<School>? schools = new List<School>();
                foreach(var  schoolDto in schoolDtos)
                {
                    School school = _mapper.Map<School>(schoolDto);

                    Village village = _addressRepository.getVillageById(schoolDto.villageCode);
                    if (village == null)
                    {
                        District district = _addressRepository.getDistrictById(schoolDto.districtCode);
    

                    Village villageCreate = new Village()
                    {
                        villageCode = Guid.NewGuid().ToString(),
                        villageName = schoolDto.VillageName,
                        district = district
                    };
                        _addressRepository.createVillage(villageCreate);
                        village = villageCreate;
                    }
                    if (village == null)
                    {
                        throw new Exception("System can't find  village");
                    }
                    school.Village = village;
                    var projectError = await _projectRepository.getProjectActiveProject();
                    if (projectError.IsError)
                    {
                        throw new Exception("System can't find project plan");
                    }
                    school.Project = projectError.Value;

                    var user = await _userManager.FindByIdAsync(schoolDto.userId);
                    if (user == null)
                    {
                        throw new Exception("Invalid user data, user id is required");
                    }
                    school.CreateBy = user;
                    var result = await _projectRepository.createSchool(school);
                    isSuccess = result.Value;
                    schools.Add(school);
                }

                if (isSuccess)
                {
                    List<SchoolResponseDto> schoolResponse = _mapper.Map<List<SchoolResponseDto>>(schools);
                    return new MessageReponse<List<SchoolResponseDto>>() {
                        isSuccess = true,
                        statusCode = 200,
                        message = "Successful",
                        data = schoolResponse
                    };
                }
                else
                {
                    return new MessageReponse<List<SchoolResponseDto>>()
                    {
                        isSuccess = false,
                        statusCode = 500,
                        message = "Fail to create school list"
                    };
                }
                

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ErrorOr<List<ProjectPlan>>> getProjects(BaseFilter filter)
        {
            try
            {
              return await   _projectRepository.getProjects(filter);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
