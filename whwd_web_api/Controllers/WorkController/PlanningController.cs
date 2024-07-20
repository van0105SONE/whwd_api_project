using ApplicationCore.Dtos.RecipientDto;
using ApplicationCore.Dtos.Work;
using ApplicationCore.Filter;
using AutoMapper;
using ErrorOr;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Users;
using Infrastructure.Model.Work;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services.Service.PositionService;
using System.Diagnostics.CodeAnalysis;
using whwd_web_api.Errors;


namespace whwd_web_api.Controllers.WorkController
{
    [ApiController]
    public class PlanningController : Controller
    {

        IMapper _mapper { get; set; }
        IProjectService _projectService { get; set; }   
        public PlanningController(DatabaseContexts dbContext, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _mapper = mapper;
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
			   return  Ok(result);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       
        [HttpPost]
        [Route("createDonateThing")]
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


        [HttpPost]
        [Route("createSchool")]
        public async Task<IActionResult> createSchool([FromBody] List<SchoolDto> schoolDto)
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
