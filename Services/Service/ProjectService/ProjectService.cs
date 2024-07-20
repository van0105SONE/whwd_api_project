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
using System.Diagnostics.CodeAnalysis;
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
        
       
        public ProjectService(UserManager<ApplicationUser> userManager, DatabaseContexts context, IMapper mapper) {
            _mapper = mapper;
            _userManager = userManager;
            _projectRepository = new ProjectPlanRepository(context);
            _addressRepository = new AddressRepository(context);
        }

        public async Task<ErrorOr<MessageReponse<ProjectPlanResponseDto>>> createProject(ProjectPlanDto projectPlanParam)
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
                    projectPlan.TotalRecieve = 0;
                    projectPlan.TotalFund = 0;
                    projectPlan.totalFundRaisedPlace = 0;
                    projectPlan.totalFundRaisedPlace = 0;
                    projectPlan.CreateBy = user;
                    var isCreated = await _projectRepository.create(projectPlan);

                   if (isCreated.Value)
                  {
                   var response = _mapper.Map<ProjectPlanResponseDto>(projectPlan);
                    return new MessageReponse<ProjectPlanResponseDto>() { 
                        statusCode = 201,
                        isSuccess = true,
                        message = "Succesful",
                        data = response
                    };

                }else
                {
                    return new MessageReponse<ProjectPlanResponseDto>()
                    {
                        statusCode = 500,
                        isSuccess = false,
                        message = "Fail"
                    };
                }


            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ErrorOr<MessageReponse<ProjectPlanResponseDto>>> deleteProjectPlan(Guid Id)
        {
            try
            {
              var isDeleted = await _projectRepository.delete(Id);
                if (isDeleted)
                {
                    return new MessageReponse<ProjectPlanResponseDto>() { 
                        statusCode = 200,
                        isSuccess = true,
                        message = "Successful"
                    };

                }
                else
                {
                    return new MessageReponse<ProjectPlanResponseDto>()
                    {
                        statusCode = 500,
                        isSuccess = true,
                        message = "Successful"
                    };
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<ErrorOr<ProjectPlan>> getActiveProjectPlan()
        {
            try
            {
                return _projectRepository.getProjectActiveProject();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
                        return Error.Validation(ErrorCodes.Validation,"System can't find  village");
                    }
                    school.village = village;
                    var projectError = await _projectRepository.getProjectActiveProject();
                    if (projectError.IsError)
                    {
                        return Error.Validation(ErrorCodes.Validation, "There is no project plan is created yet");
                    }
                    school.project = projectError.Value;

                    var user = await _userManager.FindByIdAsync(schoolDto.userId);
                    if (user == null)
                    {
                        return Error.Validation(ErrorCodes.Validation, "Invalid user data, user id is required");
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


        public async Task<ErrorOr<MessageReponse<List<SchoolResponseDto>>>> getSchools()
        {
            try
            {
               var schoolResult = await _projectRepository.getSchools();
                List<SchoolResponseDto> schools =  _mapper.Map<List<SchoolResponseDto>>(schoolResult.Value);
                return new MessageReponse<List<SchoolResponseDto>>()
                {
                    statusCode = 200,
                    isSuccess = true,
                    message = "Successful",
                    data = schools
                };
            }catch(Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ErrorOr<MessageReponse<DonateThingResponseDto>>> updateDonateThing(Guid Id, DonateThingDto donateThingDto)
        {
            try
            {
                var donateThingResult = await _projectRepository.GetDonateThingById(Id);
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
                var result = await _projectRepository.updateDonateThing(donateThing);
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
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        Task<ErrorOr<MessageReponse<DonateThingResponseDto>>> IProjectService.deleteDonateThing(Guid Id)
        {
            throw new NotImplementedException();
        }

       public async Task<ErrorOr<MessageReponse<List<DonateThingResponseDto>>>> getDonateThings(DonateThingFilter filter)
        {
            try
            {
               var projects = await _projectRepository.getProjects(filter);
               var response =   _mapper.Map<List<DonateThingResponseDto>>(projects);
                return new MessageReponse<List<DonateThingResponseDto>>() { 
                    statusCode = 200,
                    message = "Successful",
                    isSuccess = true,
                    data = response
                    };

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       public async   Task<ErrorOr<MessageReponse<List<ProjectPlanResponseDto>>>> getProjects(BaseFilter filter)
        {
            try
            {
                var result = await _projectRepository.getProjects(filter);
                return new MessageReponse<List<ProjectPlanResponseDto>>()
                {
                    statusCode = 200,
                    message = "Successful",
                    isSuccess = true
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

   public async  Task<ErrorOr<MessageReponse<ProjectPlanResponseDto>>> getProjectPlanById(Guid Id)
        {
            try
            {
                var result = await  _projectRepository.getProjectPlanById(Id);
                 var response =  _mapper.Map<ProjectPlanResponseDto>(result);
                return new MessageReponse<ProjectPlanResponseDto>()
                {
                    statusCode = 200,
                    isSuccess = true,
                    message = "Success"
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
