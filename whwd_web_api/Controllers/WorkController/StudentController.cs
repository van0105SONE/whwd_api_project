using ApplicationCore.Dtos.StudentDto;
using AutoMapper;
using ErrorOr;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Service.StudentService;

namespace whwd_web_api.Controllers.WorkController
{
    [ApiController]
    public class StudentController : Controller
    {
        private IMapper _mapper { get; set; }
        private IStudentService _studentService;
        private UserManager<ApplicationUser> _userManager { get; set; }
        public StudentController(UserManager<ApplicationUser> userManager ,DatabaseContexts context, IMapper mapper) {
            _studentService = new StudentService(context, userManager, mapper);
        }

        [HttpPost]
        [Route("createStudent")]
       async  public Task<IActionResult> createStudent([FromBody] StudentDto studentDto)
        {
            try
            {
              var result = await  _studentService.createStudent(studentDto);
                return result.Match(t => Ok(t), err => Problem(err.FirstOrDefault().Description));
            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
