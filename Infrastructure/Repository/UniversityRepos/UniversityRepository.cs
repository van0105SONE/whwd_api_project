using Infrastructure.DataBaseContext;
using Infrastructure.Model.University;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repository.UniversityRepos
{
    public class UniversityRepository : IUniversityRespository
    {
        private DatabaseContexts _DbContext { get; set; }
        public UniversityRepository(DatabaseContexts dbContext)
        {
            _DbContext = dbContext;
        }

        public Major createMajor(Major major)
        {
            try
            {
                _DbContext.majors.Add(major);
                _DbContext.SaveChanges();
                return major;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Major getMajorById(string majorId)
        {
            try
            {
                Major? major = _DbContext.majors.FirstOrDefault(t => t.Id == majorId);
                return major;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Major> getMajors()
        {
            try
            {
                List<Major> majors = _DbContext.majors.ToList();
                return majors;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<Department> getDepartment()
        {
            try
            {
                List<Department> departments = _DbContext.departments.ToList();
                return departments;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Department getDepartmentById(Guid Id)
        {
            try
            {
                Department? department = _DbContext.departments.FirstOrDefault(t => t.Id == Id);
                return department;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<University> getUniversities()
        {
            try
            {
                List<University> universities = _DbContext.university.ToList();
                return universities;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Faculty> getFaculties()
        {
            try
            {
               List<Faculty> faculties = _DbContext.faculty.Include(t => t.university).ToList();
               return faculties;
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
