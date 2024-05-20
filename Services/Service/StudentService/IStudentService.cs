using ApplicationCore.Dtos.StudentDto;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.StudentService
{
    public interface IStudentService
    {
        public Task<ErrorOr<bool>> createStudent(StudentDto studentDto);
    }
}
