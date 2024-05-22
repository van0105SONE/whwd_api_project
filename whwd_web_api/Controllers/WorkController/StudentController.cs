using ApplicationCore.Dtos;
using ApplicationCore.Dtos.StudentDto;
using ApplicationCore.Filter;
using AutoMapper;
using ErrorOr;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Student;
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
    [HttpDelete]
    [Route("deleteStudent")]
    public async Task<IActionResult> deleteStudent([FromQuery] Guid Id){
        try{
            var result = await _studentService.deleteStudent(Id);
            return result.Match(t => Ok(new MessageReponse(){
                isSuccess = t,
                message = "Delete successful"
            }), err => Problem(err.FirstOrDefault().Description));
        }catch(Exception ex){
            return Problem(ex.Message);
        }
    }

    [HttpPut]
    [Route("updateStudent")]
    public async Task<IActionResult> updateStudent([FromBody] StudentUpdateDto studentDto){
        try{
            var result = await _studentService.updateStudent(studentDto);
            return result.Match(t => Ok(new MessageReponse(){
                isSuccess = t,
                message = "Delete successful"
            }), err => Problem(err.FirstOrDefault().Description));
        }catch(Exception ex){
            return Problem(ex.Message);
        }
    }
    [HttpGet]
    [Route("getStudents")]
    async public Task<IActionResult> getStudents([FromQuery] BaseFilter filter){
     try{
        var result = await _studentService.getStudents(filter);
        return result.Match(t => Ok(t), err => Problem(err.FirstOrDefault().Description));
     }catch(Exception ex){
        return Problem(ex.Message);
     }
    }

    [HttpGet]
    [Route("GetStudentById")]
    async public Task<IActionResult>  GetStudentById([FromQuery] Guid Id){
        try{
          var result =  await _studentService.getStudentById(Id);
          return result.Match(t => Ok(t), err => Problem(err.FirstOrDefault().Description));
        }catch(Exception ex){
            return Problem(ex.Message);
        }
    }

  }
}
