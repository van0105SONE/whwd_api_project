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
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services.Service.PositionService;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;
using whwd_web_api.Errors;


namespace whwd_web_api.Controllers.WorkController
{
    [ApiController]
    public class PlanningController : Controller
    {

        IMapper _mapper { get; set; }
        IProjectService _projectService { get; set; }   
        DatabaseContexts _databaseContexts { get; set; }
        public PlanningController(DatabaseContexts dbContext, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _mapper = mapper;
            _databaseContexts = dbContext;
            _projectService = new ProjectService(userManager,dbContext, mapper);
        }


        [Route("createPlan")]
        [HttpPost]
        public async Task<IActionResult> CreateProjectPlan([FromBody] ProjectPlanDto projectDto)
        {
            try
            {
                var result =  await _projectService.createProject(projectDto);
                if (result.IsError)
                {
                    return Ok(ErrorHandler<ProjectPlanResponseDto>.HandleErrorResponse(result.FirstError.Code, result.FirstError.Description));
                }
                else
                {
                    return Ok(result.Value);
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);

            }
        }

        [Route("getProjectPlans")]
        [HttpGet]
        public async Task<IActionResult> GetProjectPLan([FromQuery] BaseFilter filter)
        {
            try
          {
                var result = await _projectService.getProjects(filter);
			   return  Ok(result.Value);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [Route("getActiveProjectPlan")]
        [HttpGet]
        public async Task<IActionResult> getActiveProjectPlan()
        {
            try
            {
               var projectPLan =   _databaseContexts.projectPlan.FirstOrDefault(t => t.IsActive);
                return Ok(new MessageReponse<ProjectPlanResponseDto>()
                {
                    statusCode = 200,
                    isSuccess = true,
                    message = "success",
                    data = _mapper.Map<ProjectPlanResponseDto>(projectPLan)
                }); ;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Route("getSchoolByProjectId/{projectId}")]
        [HttpGet]
        public async Task<IActionResult> GetSchoolByProjectId(Guid projectId,[FromQuery] BaseFilter filter)
        {
            try
            {
                var schools = _databaseContexts.schoools.Include(t => t.village).ThenInclude(t => t.district).ThenInclude(t => t.province).Where(t => t.project.Id == projectId).ToList();

                return Ok(new MessageReponse<List<SchoolResponseDto>>()
                {
                    isSuccess = true,
                    message = "Successful",
                    data = _mapper.Map<List<SchoolResponseDto>>(schools)
                }); 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpPost]
        [Route("createDonateThing/{projectId}")]
        public async Task<IActionResult> createSchool(Guid projectId, [FromBody] DonateThingDto donateThingDto)
        {
            try
            {
                var donateThing = _mapper.Map<DonateThing>(donateThingDto);
                var project = _databaseContexts.projectPlan.FirstOrDefault(t => t.Id == projectId);
                var user = _databaseContexts.Users.FirstOrDefault(t => t.Id == donateThingDto.userId);


                if (project == null)
                {
                    return BadRequest(new MessageReponse<SchoolResponseDto>()
                    {
                        statusCode = 400,
                        isSuccess = false,
                        message = "Fail to create doe server error",
                    });
                }
                else if (user == null)
                {
                    return BadRequest(new MessageReponse<SchoolResponseDto>()
                    {
                        statusCode = 400,
                        isSuccess = false,
                        message = "Fail to create school doue to user data null",
                    });
                }

                donateThing.ProjectPlan = project;
                donateThing.CreateBy = user;
                _databaseContexts.donateThings.Add(donateThing);
                _databaseContexts.SaveChanges();

                return Ok(new MessageReponse<SchoolResponseDto>()
                {
                    statusCode = 200,
                    message = "success",
                    isSuccess = true,
                    data = _mapper.Map<SchoolResponseDto>(donateThing)
                });

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }



        [HttpPost]
        [Route("createDonateThingByMany")]
        public async Task<IActionResult> createDonateThing([FromBody] List<DonateThingDto> donateThings)
        {
            try
            {
                 var result = await _projectService.createDonateThing(donateThings);
                if (result.IsError)
                {
                    return Ok(ErrorHandler<DonateThingResponseDto>.HandleErrorResponse(result.FirstError.Code, result.FirstError.Description));
                }else
                {
                    return Ok(result.Value);
                }
            }catch(Exception ex)
            {
                return Problem(ex.Message); 
            }
        }


        [HttpPatch]
        [Route("updateDonateThing/{Id}")]
        public async Task<IActionResult> updateDonateThing(Guid Id,[FromBody] DonateThingDto donateThing)
        {
            try
            {
                if (Id == Guid.Empty)
                {

                    return BadRequest("Donate thing id is invalid, Id is required");
                }

                var result = await _projectService.updateDonateThing(Id,donateThing);
                if (result.IsError)
                {
                    return Ok(ErrorHandler<DonateThingResponseDto>.HandleErrorResponse(result.FirstError.Code, result.FirstError.Description));
                }else
                {
                    return Ok(result.Value);
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deleteDonateThing")]
        public async Task<IActionResult> deleteDonateThing([FromQuery] Guid Id)
        {
            try
            {
                var result = await _projectService.deleteDonateThing(Id);
                if (result.IsError)
                {
                    return Ok(ErrorHandler<DonateThingResponseDto>.HandleErrorResponse(result.FirstError.Code, result.FirstError.Description));
                }
                else
                {
                    return Ok(result.Value);
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }



        [HttpGet]
        [Route("getDonateThings")]
        public async Task<IActionResult> getDonateThing([FromQuery] DonateThingFilter filter)
        {
            try
            {
                var result = await _projectService.getDonateThings(filter);
                return result.Match(t => Ok(t), err => Problem(err.FirstOrDefault().Description));
            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("getDonateThingsByProjectId/{projectId}")]
        public async Task<IActionResult> getDonateThingByProjectPlan(Guid projectId )
        {
            try
            {
                var donateThings  = _databaseContexts.donateThings.Where( t => t.ProjectPlan.Id == projectId).ToList();
                return Ok(new MessageReponse<List<DonateThingResponseDto>>()
                {
                    isSuccess = true,
                    message = "Successful",
                    data = _mapper.Map<List<DonateThingResponseDto>>(donateThings)
                });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpPost]
        [Route("createSchool/{projectId}")]
        public async Task<IActionResult> createSchool(Guid projectId,[FromBody] SchoolDto schoolDto)
        {
            try
            {
                School school = _mapper.Map<School>(schoolDto);

               var project =  _databaseContexts.projectPlan.FirstOrDefault(t => t.Id == projectId);
               var village = _databaseContexts.villages.FirstOrDefault(t => t.villageCode ==  schoolDto.villageCode);
                var district = _databaseContexts.districts.FirstOrDefault(t => t.districtCode == schoolDto.districtCode);
                var user = _databaseContexts.Users.FirstOrDefault(t => t.Id == schoolDto.userId);
                if (district == null)
                {
                    return BadRequest(new MessageReponse<SchoolResponseDto>()
                    {
                        statusCode = 400,
                        isSuccess = false,
                        message = "System can't district data",
                    });
                }


                if (village == null)
                {
                    var newVillage = new Village() {
                        villageCode = Guid.NewGuid().ToString(),
                        villageName = schoolDto.Name,
                        district = district
                    };

                     _databaseContexts.villages.Add(newVillage);
                    _databaseContexts.SaveChanges();
                    village = newVillage;
                }
                if (project == null)
                {
                    return BadRequest(new MessageReponse<SchoolResponseDto>() {
                        statusCode = 400,
                        isSuccess = false,
                        message = "Fail to create doe server error",
                       });
                }else if (user == null)
                {
                    return BadRequest(new MessageReponse<SchoolResponseDto>()
                    {
                        statusCode = 400,
                        isSuccess = false,
                        message = "Fail to create school doue to user data null",
                    });
                }
                school.village = village;
                school.CreateBy = user;
                school.project = project;
                _databaseContexts.schoools.Add(school);
                _databaseContexts.SaveChanges();

                return Ok(new MessageReponse<SchoolResponseDto>() {
                    statusCode = 200,
                   message = "success",
                   isSuccess = true,
                   data = _mapper.Map<SchoolResponseDto>(school)
                });

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpPost]
        [Route("createSchoolByMany")]
        public async Task<IActionResult> createSchoolByMany([FromBody] List<SchoolDto> schoolDto)
        {
            try
            {
                var result = await   _projectService.createSchool(schoolDto);
                if (result.IsError)
                {
                    return Ok(ErrorHandler<SchoolResponseDto>.HandleErrorResponse(result.FirstError.Code, result.FirstError.Description));
                }
                else
                {
                    return Ok(result.Value);
                }
            }
            catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("getSchools")]
        public async Task<IActionResult> getSchools()
        {
            try
            {
                var result = await _projectService.getSchools();
                return result.Match(t => Ok(t), err => Problem(err.FirstOrDefault().Description));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
