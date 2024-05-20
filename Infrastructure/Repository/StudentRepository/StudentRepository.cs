using ErrorOr;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Student;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repository.StudentRepository
{
    public class StudentRepository : IStudentRepository
    {
        private DatabaseContexts _dbContext { get; set; }
        public StudentRepository(DatabaseContexts dbContext)
        {
            _dbContext = dbContext;
        }
       async public Task<ErrorOr<bool>> create(Student student)
        {
            try
            {
                _dbContext.students.Add(student);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        async public void delete(Guid Id)
        {
            try
            {
                Student? student = await _dbContext.students.FirstOrDefaultAsync();
                if (student == null)
                {
                    throw new Exception("Error, Student information isn't found");
                }
                _dbContext.students.Remove(student);
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        async public void update(Student studentParam)
        {
            try
            {
                _dbContext.students.Update(studentParam);
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }
    }
}
