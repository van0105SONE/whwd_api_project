using Infrastructure.Model.University;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.UniversityRepos
{
    public interface IUniversityRespository
    {
        public Major createMajor(Major major);
        public List<Major> getMajors();
        public Major getMajorById(string majorId);
        public List<Department> getDepartment();
        public Department getDepartmentById(Guid Id);
        public List<University> getUniversities();

        public List<Faculty> getFaculties();
    }
}
