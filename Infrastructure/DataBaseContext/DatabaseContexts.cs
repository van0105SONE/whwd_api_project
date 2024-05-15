using Infrastructure.Model.Address;
using Infrastructure.Model.Student;
using Infrastructure.Model.University;
using Infrastructure.Model.Users;
using Infrastructure.Model.Work;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.DataBaseContext
{
    public class DatabaseContexts : IdentityDbContext<ApplicationUser>
    {
        public DatabaseContexts(DbContextOptions options):base(options) 
        { 
        }


       public DbSet<Position> positions { get; set; }
       public DbSet<UserType> userTypes { get; set; }
       public DbSet<PositionTeam> position_teams { get; set; }
       public DbSet<Position> position { get; set; }


       public DbSet<ProjectTeam> project_teams { get; set; }
       public DbSet<Major>   majors { get; set; }
       public DbSet<Department> departments { get; set; }
       public DbSet<Faculty> faculty { get; set; }
       public DbSet<University> university { get; set; }
       public DbSet<ProjectPlan> projectPlan { get; set; }
       public DbSet<DonateThing> donateThings { get; set; }
       public DbSet<Student> students { get; set; }

       
        // address 
        public DbSet<Province> provinces { get; set; }
        public DbSet<District> districts { get; set; }
        public DbSet<Village> villages { get; set; }
    }
}
