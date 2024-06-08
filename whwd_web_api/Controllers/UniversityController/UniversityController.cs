using Infrastructure.DataBaseContext;
using Infrastructure.Model.University;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Service.AddressService;
using Services.Service.UniversityService;

namespace whwd_web_api.Controllers.UniversityController
{
    [ApiController]
    public class UniversityController : Controller
    {
        private DatabaseContexts _dbContext { get; set; }
        private IUniversityService _universityService { get; set; }
        public UniversityController(DatabaseContexts context)
        {
            _dbContext = context;
            _universityService = new UniversityService(context);
        }


        [HttpGet]
        [Route("getUnivesities")]
        public IActionResult getUniversity()
        {
            try
            {
                List<University> villages = _universityService.getUniversities();
                return Ok(villages);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpGet]
        [Route("getFaculties")]
        public IActionResult getFaculties()
        {
            try
            {
                List<Faculty> villages = _universityService.getFaculties();
                return Ok(villages);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("getDepartments")]
        public IActionResult getDepartments()
        {
            try
            {
                List<Department> villages = _universityService.getDepartment();
                return Ok(villages);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
