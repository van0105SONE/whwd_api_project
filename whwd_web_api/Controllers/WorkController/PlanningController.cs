using ApplicationCore.Dtos.Work;
using ApplicationCore.Filter;
using AutoMapper;
using ErrorOr;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Users;
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


        [Route("/create-plan")]
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
    }
}
