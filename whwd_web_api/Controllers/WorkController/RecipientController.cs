using ApplicationCore.Dtos;
using ApplicationCore.Dtos.Recipient;
using ApplicationCore.Dtos.StudentDto;
using ApplicationCore.Filter;
using AutoMapper;
using ErrorOr;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Place;
using Infrastructure.Model.Student;
using Infrastructure.Model.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Service.StudentService;
using whwd_web_api.Errors;

namespace whwd_web_api.Controllers.WorkController
{
    [ApiController]
    public class RecipientController : Controller
    {
        private IMapper _mapper { get; set; }
        private IStudentService _studentService;
        private UserManager<ApplicationUser> _userManager { get; set; }
        public RecipientController(UserManager<ApplicationUser> userManager ,DatabaseContexts context, IMapper mapper) {
            _studentService = new StudentService(context, userManager, mapper);
        }

      [HttpPost]
     [Route("createRecipient")]
       async  public Task<IActionResult> createStudent([FromBody] RecipientDto studentDto)
        {
            try
            {
                var result = await  _studentService.createStudent(studentDto);
                if (result.IsError)
                {
                    return Ok(ErrorHandler<RecipientReponseDto>.HandleErrorResponse(result.FirstError.Code, result.FirstError.Description));
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
    [HttpDelete]
    [Route("deleteRecipient/{Id}")]
    public async Task<IActionResult> deleteStudent( Guid Id){
        try{
            var result = await _studentService.deleteStudent(Id);
            return result.Match(t => Ok(new MessageReponse<Recipient>(){
                isSuccess = t,
                message = "Delete successful"
            }), err => Problem(err.FirstOrDefault().Description));
        }catch(Exception ex){
            return Problem(ex.Message);
        }
    }

    [HttpPut]
    [Route("updateRecipient/{recipientId}")]
    public async Task<IActionResult> updateStudent(Guid recipientId,[FromBody] RecipientDto studentDto){
        try{
            var result = await _studentService.updateStudent(recipientId,studentDto);
                if (result.IsError)
                {
                    return Ok(ErrorHandler<PlaceResponseDto>.HandleErrorResponse(result.FirstError.Code, result.FirstError.Description));
                }
                else
                {
                    return Ok(result.Value);
                }
            }
            catch(Exception ex){
            return Problem(ex.Message);
        }
    }
    [HttpGet]
    [Route("getRecipient")]
    async public Task<IActionResult> getStudents([FromQuery] BaseFilter filter){
     try{
        var result = await _studentService.getStudents(filter);
        return result.Match(t => Ok(t), err => Problem(err.FirstOrDefault().Description));
     }catch(Exception ex){
        return Problem(ex.Message);
     }
    }

    [HttpGet]
    [Route("GetRecipientById")]
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
