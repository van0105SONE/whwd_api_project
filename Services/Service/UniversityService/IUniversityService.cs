using Infrastructure.Model.University;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.UniversityService
{
    public interface IUniversityService
    {
        public Major createMajor(Major major);
        public List<Major> getMajors();
        public Major getMajorById(Guid majorId);
        public List<Department> getDepartment();
        public Department getDepartmentById(Guid Id);

        public List<University> getUniversities();

        public List<Faculty> getFaculties();


    }
}
