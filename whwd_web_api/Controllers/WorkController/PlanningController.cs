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
using Services.Service.PositionService;


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
                return result.Match(t => Ok(t), err => Problem(err.FirstOrDefault().Description));
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
               var responseData = _mapper.Map<List<ProjectPlanResponseDto>>(result.Value.ToList());
			   return  Ok(responseData);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       
        [HttpPost]
        [Route("createDonateThing")]
        public async Task<IActionResult> createDonateThing([FromBody] DonateThingDto donateThing)
        {
            try
            {
                 var result = await _projectService.createDonateThing(donateThing);
                return result.Match(t => Ok(t), err => Problem(err.FirstOrDefault().Description));
            }catch(Exception ex)
            {
                return Problem(ex.Message); 
            }
        }


        [HttpPatch]
        [Route("updateDonateThing")]
        public async Task<IActionResult> updateDonateThing([FromBody] UpdateDonateThingDto donateThing)
        {
            try
            {
                var result = await _projectService.updateDonateThing(donateThing);
                return result.Match(t => Ok(t), err => Problem(err.FirstOrDefault().Description));
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
                return result.Match(t => Ok(t), err => Problem(err.FirstOrDefault().Description));
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
        public async Task<IActionResult> createSchool([FromBody] SchoolDto schoolDto)
        {
            try
            {
                var result = await   _projectService.createSchool(schoolDto);
                return result.Match(t => CreatedAtAction(nameof(createSchool), t), err => Problem(err.FirstOrDefault().Description));
            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
