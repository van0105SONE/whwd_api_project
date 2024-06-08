using Infrastructure.Model.Account;
using Infrastructure.Model.Address;
using Infrastructure.Model.Donate;
using Infrastructure.Model.Place;
using Infrastructure.Model.Recipient;
using Infrastructure.Model.Student;
using Infrastructure.Model.University;
using Infrastructure.Model.Users;
using Infrastructure.Model.Work;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.DataBaseContext
{
    public class DatabaseContexts : IdentityDbContext<ApplicationUser, IdentityRole, string>
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
       public DbSet<School> schoools { get; set; }

        // address 
        public DbSet<Province> provinces { get; set; }
        public DbSet<District> districts { get; set; }
        public DbSet<Village> villages { get; set; }

        //recipient
        public DbSet<Recipient> students { get; set; }

        //fundraising place
        public DbSet<FundRaisingPlace> fundRaisingPlaces { get; set; }
    

        //donation
        public DbSet<Donator> donators { get; set;}
        public DbSet<Donation> Donation { get; set; }

        //#done: need to fix
        //Account
        public DbSet<Account> accounts { get; set; }
        public DbSet<AccountType> accountTypes { get; set; }

        //transaction
        public DbSet<Transaction> transactions { get; set; }
        public DbSet<TransactionType> transactionTypes { get; set; }
        public DbSet<SourceType> sourceTypes { get; set; }

        //conjoint
        public DbSet<Conjoint> conjoints { get; set; }
    }
}
