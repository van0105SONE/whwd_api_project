using ApplicationCore.Filter;
using ErrorOr;
using Infrastructure.Model.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.StudentRepository
{
    public interface IStudentRepository
    {
        Task<ErrorOr<bool>> create(Student student);
        Task<List<Student>>  getStudents(BaseFilter filter); 
        Task<bool> delete(Guid Id);
        Task<bool> update(Student student);

        Task<Student> GetStudentById(Guid Id);
    }
}
