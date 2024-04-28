using Infrastructure.Model.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.IRepository
{
    public interface IStudentRepository
    {
        void create(Student student);
        void delete(Guid Id);
        void update(Student student);
    }
}
