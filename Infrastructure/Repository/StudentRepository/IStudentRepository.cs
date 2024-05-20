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
        void delete(Guid Id);
        void update(Student student);
    }
}
