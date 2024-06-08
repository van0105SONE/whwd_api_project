using ApplicationCore.Constanst;
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
using Microsoft.VisualBasic;
using Services.Middleware;

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
                    return Error.NotFound("NotFound", "User ist found on system"); 
                }
                bool isAllowed = await roleMiddleWare.IsUserAllowed(user, Constant.CREATEROLE, Constant.POSITIONSTARTPROJECT);
                if (isAllowed)
                {
                    projectPlan.IsActive = true;
                    projectPlan.valueInKip = 0;
                    projectPlan.ValueInBath = 0;
                    projectPlan.ValueInBath = 0;
                    projectPlan.CreateBy = user;
                    return await _projectRepository.create(projectPlan);
                }else
                {
                   return Error.Unauthorized("Unauthorized", "User Role or Position isn't allowed to create");
                }

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

        public async Task<ErrorOr<bool>> createDonateThing(DonateThingDto donateThingDto)
        {
            try
            {
                DonateThing donateThing = _mapper.Map<DonateThing>(donateThingDto);
                donateThing.CreateBy = await _userManager.FindByIdAsync(donateThingDto.userId);
                var projectResult = await _projectRepository.getProjectActiveProject();
                if (projectResult.Value == null)
                {
                    return Error.NotFound("NotFound", "project is not found");
                }
                donateThing.ProjectPlan = projectResult.Value;
                var result = await  _projectRepository.createDonateThing(donateThing);
                return result;
            }catch(Exception ex)
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

        public async Task<ErrorOr<bool>> updateDonateThing(UpdateDonateThingDto donateThingDto)
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
                donateThing.personAmount = donateThing.personAmount;
                var result =  await _projectRepository.updateDonateThing(donateThing);
                return result;
            }catch(Exception e)
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

        public async Task<ErrorOr<bool>> createSchool(SchoolDto schoolDto)
        {
            try
            {
                School school  = _mapper.Map<School>(schoolDto);
                Village village = _addressRepository.getVillageById(schoolDto.villageCode);
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

               var user =  await _userManager.FindByIdAsync(schoolDto.userId);
                if(user == null)
                {
                    throw new Exception("Invalid user data, user id is required");
                }
                school.CreateBy = user;
                var result =  await  _projectRepository.createSchool(school);
                return result;
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
