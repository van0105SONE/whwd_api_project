using Infrastructure.DataBaseContext;
using Infrastructure.Model.University;
using Infrastructure.Repository.UniversityRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.UniversityService
{
    public class UniversityService : IUniversityService
    {
        private IUniversityRespository universityRepository { get; set; }
        public UniversityService(DatabaseContexts dbContext) {
            universityRepository = new UniversityRepository(dbContext);
        }

        public Major createMajor(Major major)
        {
            throw new NotImplementedException();
        }

        public List<Department> getDepartment()
        {
            try
            {
               return  universityRepository.getDepartment();
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

                return  universityRepository.getDepartmentById(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Major getMajorById(Guid majorId)
        {
            throw new NotImplementedException();
        }

        public List<Major> getMajors()
        {
            try
            {
                return universityRepository.getMajors();
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
                return universityRepository.getUniversities();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Faculty> getFaculties()
        {
            try
            {
                return universityRepository.getFaculties();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
