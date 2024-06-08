using ApplicationCore.Dtos.StudentDto;
using ApplicationCore.Filter;
using ErrorOr;
using Infrastructure.Model.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.StudentService
{
    public interface IStudentService
    {
        public Task<ErrorOr<bool>> createStudent(RecipientDto studentDto);
        public Task<ErrorOr<bool>> updateStudent(StudentUpdateDto studentDto);
        public Task<ErrorOr<bool>> deleteStudent(Guid id); 

        public Task<ErrorOr<Recipient>> getStudentById(Guid Id);  
        public Task<ErrorOr<List<Recipient>>> getStudents(BaseFilter filter);
    }
}
