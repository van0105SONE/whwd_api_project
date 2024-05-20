using ApplicationCore.Dtos.StudentDto;
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
                ApplicationUser user =  await  _userManager.FindByIdAsync(studentDto.userId);
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
    }
}
