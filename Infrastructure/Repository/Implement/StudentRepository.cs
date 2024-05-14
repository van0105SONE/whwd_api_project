using Infrastructure.DataBaseContext;
using Infrastructure.Model.Student;
using Infrastructure.Repository.IRepository;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repository.Implement
{
    public class StudentRepository : IStudentRepository
    {
        private DatabaseContexts _dbContext { get; set; }
        public StudentRepository(DatabaseContexts dbContext) { 
              _dbContext = dbContext;
        }
        public void create(Student student)
        {
            try
            {
                _dbContext.students.Add(student);
            }catch(Exception error) {
                throw error;
            }
        }

       async public void delete(Guid Id)
        {
            try
            {
               Student? student =   await  _dbContext.students.FirstOrDefaultAsync();
                if(student == null)
                {
                    throw new Exception("Error, Student information isn't found");
                }
                _dbContext.students.Remove(student);
            }catch(Exception error)
            {
                throw error;
            }
        }

        async public void update(Student studentParam)
        {
            try
            {
               Student? student = await _dbContext.students.FirstOrDefaultAsync(t => t.Id == studentParam.Id);
                if (student == null)
                {
                     throw  new Exception($"Student {studentParam.Id} isn't found");
                }


                student.Name = studentParam.Name;
                student.birthDate = studentParam.birthDate;
                student.Year = studentParam.Year;
                student.ShirtSize = studentParam.ShirtSize;
                student.SkirtSize = studentParam.SkirtSize;
                student.ShoesSize = studentParam.ShoesSize;

               _dbContext.students.Update(student);
            }catch(Exception error)
            {
                throw new Exception(error.Message);
            }
        }
    }
}
