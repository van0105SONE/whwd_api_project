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
        Task<ErrorOr<bool>> create(Recipient student);
        Task<List<Recipient>>  getStudents(BaseFilter filter); 
        Task<bool> delete(Guid Id);
        Task<bool> update(Recipient student);

        Task<Recipient> GetStudentById(Guid Id);
    }
}
