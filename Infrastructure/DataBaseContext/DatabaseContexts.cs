using Infrastructure.Model;
using Infrastructure.Model.Student;
using Infrastructure.Model.Volunteer;
using Infrastructure.Model.Work;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.DataBaseContext
{
    public class DatabaseContexts : IdentityDbContext<ApplicationUser>
    {
        public DatabaseContexts(DbContextOptions options):base(options) 
        { 

        }

       public DbSet<Department> departments { get; set; }
       public DbSet<Major>   majors { get; set; }
       public DbSet<Faculty> faculty { get; set; }
       public DbSet<Generation> generations { get; set; }
       public DbSet<ProjectPlan> projectPlan { get; set; }
       public DbSet<DonateThing> donateThings { get; set; }
       public DbSet<Student> students { get; set; }

    }
}
