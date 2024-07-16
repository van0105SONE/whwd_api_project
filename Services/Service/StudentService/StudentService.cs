using ApplicationCore.Constanst;
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
using Infrastructure.Model.Work;
using Infrastructure.Repository.ProjectRepository;
using Infrastructure.Repository.StudentRepository;
using Microsoft.AspNetCore.Identity;


namespace Services.Service.StudentService
{
    public class StudentService : IStudentService
    {
        IMapper _mapper;
        public IStudentRepository _studentRepository { get; set; }
        public IProjectPlanRepository _projectService { get; set; }
        public UserManager<ApplicationUser> _userManager { get; set; }
        public StudentService(DatabaseContexts context, UserManager<ApplicationUser> userManager, IMapper mapper) {
            _studentRepository = new StudentRepository(context);
            _projectService = new ProjectPlanRepository(context);
            _userManager = userManager;
            _mapper = mapper;
        }

      async  public Task<ErrorOr<MessageReponse<RecipientReponseDto>>> createStudent(RecipientDto studentDto)
        {
            try
            {
                Recipient studentData =  _mapper.Map<Recipient>(studentDto);
                var projectPlanResult = await _projectService.getProjectActiveProject();
                var schoolResult = await _projectService.getSchoolById(studentDto.schoolId);
                if (projectPlanResult.Value == null)
                {
                    return Error.Validation(ErrorCodes.Validation, "Project isn't found");
                } else if (schoolResult.Value == null)
                {
                    return Error.Validation(ErrorCodes.Validation, "school isn't found");
                }
                ProjectPlan projectPlan = projectPlanResult.Value;
                ApplicationUser? user =  await  _userManager.FindByIdAsync(studentDto.userId);

                if (user == null)
                {
                    return Error.Validation(ErrorCodes.Validation, "User isn't found");
                }

                studentData.CreateBy = user;
                studentData.Project = projectPlan;
                studentData.School = schoolResult.Value;
                var result = await  _studentRepository.create(studentData);
                if (result.Value)
                {
                    RecipientReponseDto response = _mapper.Map<RecipientReponseDto>(studentData);
                    return new MessageReponse<RecipientReponseDto>()
                    {
                        isSuccess = true,
                        statusCode = 200,
                        message = "Successful",
                        data = response
                    };
                }
                else
                {
                    return new MessageReponse<RecipientReponseDto>()
                    {
                        isSuccess = false,
                        statusCode = 500,
                        message = "Fail to create fund raising place"
                    };
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       async public Task<ErrorOr<MessageReponse<RecipientReponseDto>>> updateStudent(Guid Id,RecipientDto studentDto)
        {
           try{
                Recipient studentData =  _mapper.Map<Recipient>(studentDto);
   
                Recipient student = await _studentRepository.GetStudentById(Id);


                var projectPlanResult = await _projectService.getProjectActiveProject();
                if (projectPlanResult.Value == null)
                {
                    return Error.Validation(ErrorCodes.Validation, "Project isn't found");
                }
                ProjectPlan projectPlan = projectPlanResult.Value;
                ApplicationUser? user =  await  _userManager.FindByIdAsync(studentDto.userId);
                if (user == null)
                {
                    return Error.Validation(ErrorCodes.Validation, "User isn't found");
                }


                student.fname = studentData.fname;
                student.lname = studentData.lname;
                student.birthDate = studentData.birthDate;
                student.level = studentData.level;
                student.UpdateBy = user;
                student.UpdateAt = DateTime.UtcNow;
                student.Project = projectPlan;
                var result = await  _studentRepository.update(student);

                if (result)
                {
                    RecipientReponseDto response = _mapper.Map<RecipientReponseDto>(studentData);
                    return new MessageReponse<RecipientReponseDto>()
                    {
                        isSuccess = true,
                        statusCode = 200,
                        message = "Successful",
                        data = response
                    };
                }
                else
                {
                    return new MessageReponse<RecipientReponseDto>()
                    {
                        isSuccess = false,
                        statusCode = 500,
                        message = "Fail to create fund raising place"
                    };
                }
            
           }catch(Exception ex){
                throw new Exception(ex.Message);
           }
        }

       async public Task<ErrorOr<bool>> deleteStudent(Guid id)
        {
             try{
                return await  _studentRepository.delete(id);
             }catch(Exception ex){
                throw new Exception(ex.Message);    
             }
        }

      async  public Task<ErrorOr<List<Recipient>>> getStudents(BaseFilter filter)
        {
             try{
               var student = await _studentRepository.getStudents(filter);
               return student;
             }catch(Exception ex){
                return Error.Unexpected("Failure", ex.Message);
             }
        }

        public async Task<ErrorOr<Recipient>> getStudentById(Guid Id)
        {
            try{
              return await _studentRepository.GetStudentById(Id);
            }catch(Exception ex){
             throw new Exception(ex.Message);
            }
        }
    }
}
