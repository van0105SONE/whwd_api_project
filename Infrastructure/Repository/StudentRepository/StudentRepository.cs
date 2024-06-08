using System.Formats.Asn1;
using ApplicationCore.Filter;
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
       async public Task<ErrorOr<bool>> create(Recipient student)
        {
            try
            {
              await  _dbContext.students.AddAsync(student);
              await  _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        async public Task<bool> delete(Guid Id)
        {
            try
            {
                Recipient? student = await _dbContext.students.FirstOrDefaultAsync();
                if (student == null)
                {
                    throw new Exception("Error, Student information isn't found");
                }
                _dbContext.students.Remove(student);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        async public Task<bool> update(Recipient studentParam)
        {
            try
            {
                 _dbContext.students.Update(studentParam);
                 await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        async public Task<List<Recipient>> getStudents(BaseFilter filter)
        {
            try{
              return await _dbContext.students.Skip((filter.page - 1) * filter.pageSize).Take(filter.pageSize).ToListAsync();
            }catch(Exception ex){
              throw new Exception(ex.Message);
            }
        }

        public async Task<Recipient> GetStudentById(Guid Id)
        {
            try{
              return await _dbContext.students.FirstAsync(t => t.Id == Id);
            }catch(Exception ex){
             throw new Exception(ex.Message);
            }
        }
    }
}
