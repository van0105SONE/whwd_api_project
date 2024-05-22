using ApplicationCore.Dtos.StudentDto;
using ApplicationCore.Filter;
using AutoMapper;
using ErrorOr;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Student;
using Infrastructure.Model.Users;
using Infrastructure.Model.Work;
using Infrastructure.Repository.ProjectRepository;
using Infrastructure.Repository.StudentRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Services.Service.PositionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

      async  public Task<ErrorOr<bool>> createStudent(StudentDto studentDto)
        {
            try
            {
                Student studentData =  _mapper.Map<Student>(studentDto);
                var projectPlanResult = await _projectService.getProjectActiveProject();
                if (projectPlanResult.Value == null)
                {
                    return Error.NotFound("NotFound", "Project isn't found");
                }
                ProjectPlan projectPlan = projectPlanResult.Value;
                ApplicationUser? user =  await  _userManager.FindByIdAsync(studentDto.userId);
                if (user == null)
                {
                    return Error.NotFound("NotFound", "User isn't found");
                }

                studentData.CreateBy = user;
                studentData.Project = projectPlan;
                var result = await  _studentRepository.create(studentData);
                return result;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       async public Task<ErrorOr<bool>> updateStudent(StudentUpdateDto studentDto)
        {
           try{
                Student studentData =  _mapper.Map<Student>(studentDto);
   
                Student student = await _studentRepository.GetStudentById(studentData.Id);
                var projectPlanResult = await _projectService.getProjectActiveProject();
                if (projectPlanResult.Value == null)
                {
                    return Error.NotFound("NotFound", "Project isn't found");
                }
                ProjectPlan projectPlan = projectPlanResult.Value;
                ApplicationUser? user =  await  _userManager.FindByIdAsync(studentDto.userId);
                if (user == null)
                {
                    return Error.NotFound("NotFound", "User isn't found");
                }
                student.fname = studentData.fname;
                student.lname = studentData.lname;
                student.SkirtSize = studentData.SkirtSize;
                student.ShirtSize = studentData.ShirtSize;  
                student.ShirtSize = studentData.ShirtSize;  
                student.ShoesSize = studentData.ShoesSize;
                student.birthDate = studentData.birthDate;
                student.level = studentData.level;
                student.UpdateBy = user;
                student.UpdateAt = DateTime.UtcNow;
                student.Project = projectPlan;
                return await  _studentRepository.update(student);
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

      async  public Task<ErrorOr<List<Student>>> getStudents(BaseFilter filter)
        {
             try{
               var student = await _studentRepository.getStudents(filter);
               return student;
             }catch(Exception ex){
                return Error.Unexpected("Failure", ex.Message);
             }
        }

        public async Task<ErrorOr<Student>> getStudentById(Guid Id)
        {
            try{
              return await _studentRepository.GetStudentById(Id);
            }catch(Exception ex){
             throw new Exception(ex.Message);
            }
        }
    }
}
