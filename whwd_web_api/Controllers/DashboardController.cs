using ApplicationCore.Dtos.Dashboard;
using AutoMapper;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Service.PositionService;
using System.Reflection;

namespace whwd_web_api.Controllers
{
    [ApiController]
    public class DashboardController : Controller
    {
        IProjectService _projectService { get; set; }
        IMapper _mapper { get; set; }
        public DashboardController(DatabaseContexts dbContext, UserManager<ApplicationUser> userManager, IMapper mapper) {
            _projectService = new ProjectService(userManager, dbContext, mapper);
            _mapper = mapper;
        }

        [Route("GetData")]
        [HttpGet]
        public async Task<IActionResult> GetDashboardData()
        {
            try
            {
               var currentProjectPlan = await  _projectService.getActiveProjectPlan();
               var school = await _projectService.getSchools();
               DashboardDataResponseDto response = new DashboardDataResponseDto();

                response.remainingDate = DateTime.Now.CompareTo(currentProjectPlan.Value.StartDate);
                response.totalFund = currentProjectPlan.Value.TotalFund;
                response.totalRecieve = currentProjectPlan.Value.TotalRecieve;
                response.fundraisedPlace = currentProjectPlan.Value.totalFundRaisedPlace;
                response.schools = _mapper.Map<List<SchoolSerieResponseDto>>(school.Value.data);

                return Ok(response);
            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Route("getStatisticData")]
        [HttpGet]
        public async Task<IActionResult> GetStatisticData()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
