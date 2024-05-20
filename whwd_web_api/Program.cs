using Infrastructure.DataBaseContext;
using Infrastructure.Model.University;
using Infrastructure.Model.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;

internal class Program
{
    private static  void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<DatabaseContexts>(option => option.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
        builder.Services.AddDefaultIdentity<ApplicationUser>(option => option.SignIn.RequireConfirmedEmail = false).AddRoles<IdentityRole>().AddEntityFrameworkStores<DatabaseContexts>();


        builder.Services.AddAutoMapper(typeof(Program));
        builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
        {
            builder.WithOrigins("*");
            builder.AllowAnyMethod();
            builder.AllowAnyHeader();
        }));



        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(jwt =>
        {
        var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtConfig:Secret").Value);
        jwt.SaveToken = true;
            jwt.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ValidIssuer = builder.Configuration.GetSection("JwtConfig:Issuer").Value
            };
        });






        var app = builder.Build();

        using (var scope =app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContexts>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //default add project team
            List<ProjectTeam> teams = new List<ProjectTeam> {
                new ProjectTeam()
            {
               Id = Guid.NewGuid(),
               RefNO = DateTime.Now.ToString(),
               Name = "Accountant Team"
            },
            new ProjectTeam()
            {
               Id = Guid.NewGuid(),
               RefNO = DateTime.Now.ToString(),
               Name = "Coordinator Team"
            },
                        new ProjectTeam()
            {
               Id = Guid.NewGuid(),
               RefNO = DateTime.Now.ToString(),
               Name = "Art & Media Team"
            },
            new ProjectTeam()
            {
               Id = Guid.NewGuid(),
               RefNO = DateTime.Now.ToString(),
               Name = "Stock Team"

            }
            };

            foreach(var team in teams)
            {
                var teamss =  dbContext.project_teams.ToList();
                Console.Write($@"=======> {teamss}");
                bool isExist = dbContext.project_teams.Any(t => t.Name.ToLower().Replace(" ", "") == team.Name.ToLower().Replace(" ", ""));
                if (!isExist)
                {
                    dbContext.project_teams.Add(team);
                    dbContext.SaveChanges();
                }
            }



            List<UserType> userTypes = new List<UserType>() {
                new UserType(){ 
                  Id = Guid.NewGuid().ToString(),
                  Name = "Staff",
                },
                new UserType(){
                  Id = Guid.NewGuid().ToString(),
                  Name = "Borum Volunteer",
                },
                new UserType(){
                  Id = Guid.NewGuid().ToString(),
                  Name = "Co-Volunteer",
                }
            };



            foreach (var userType in userTypes)
            {
              bool isExist =  dbContext.userTypes.Any(x => x.Name == userType.Name);
                if(!isExist)
                {
                    dbContext.userTypes.Add(userType);
                    dbContext.SaveChanges();
                }
            }

            List<Position> positions = new List<Position>() {
               new Position()
               {
                   Id = Guid.NewGuid(),
                   RefNo = "    P01",
                   PositionName = "Project Manager",

               },
               new Position()
               {
                   Id = Guid.NewGuid(),
                   RefNo = "L01",
                   PositionName = "Leader",
                   
               },
              new Position()
               {
                   Id = Guid.NewGuid(),
                   RefNo = "M01",
                   PositionName = "Member",

               }
            };


            foreach (var position in positions)
            {
                bool isExist = dbContext.position.Any(x => x.PositionName == position.PositionName);
                if (!isExist)
                {
                    dbContext.position.Add(position);
                    dbContext.SaveChanges();
                }
            }

            List<University> universities = new List<University>()
            {
                new University()
                {
                    Id = Guid.NewGuid(),
                    Name = "National University of laos"
                },
                               new University()
                {
                    Id = Guid.NewGuid(),
                    Name = "ວິທະຍາໄລວິທະຍາສາດສຸຂະພາບ"
                },
              new University {
                    Id = Guid.NewGuid(),
                    Name = "ວິທະຍາໄລວິສຸດສະກະ"
                }
            };

            foreach(var university in universities)
            {
                 bool isExist =  dbContext.university.Any(t => t.Name == university.Name);
                if(!isExist)
                {
                    dbContext.university.Add(university);
                    dbContext.SaveChanges();
                }
            }


          University? university1 =   dbContext.university.FirstOrDefault(t => t.Name.Replace(" ", "").ToLower() == "National University of laos".Replace(" ", "").ToLower());

            List<Faculty> faculties = new List<Faculty>()
            {
                new Faculty()
                {
                    Id = Guid.NewGuid(),
                    Name = "Natural Sciennce",
                  university =   university1

                },
                               new Faculty()
                {
                    Id = Guid.NewGuid(),
                    Name = "Social Science",
                    university = university1
                },

            };

            foreach (var faculty1 in faculties)
            {
                bool isExist = dbContext.faculty.Any(t => t.Name == faculty1.Name);
                if (!isExist)
                {
                    dbContext.faculty.Add(faculty1);
                    dbContext.SaveChanges();
                }
            }




            List<Department> departments = new List<Department>()
            {
                new Department()
                {
                    Id = Guid.NewGuid(),
                    Name = "Natural Sciennce",
                  faculty =   faculties[0]

                },
                new Department()
                {
                    Id = Guid.NewGuid(),
                    Name = "Social Science",
                    faculty = faculties[0]
                },

            };

            foreach (var department in departments)
            {
                bool isExist = dbContext.departments.Any(t => t.Name == department.Name);
                if (!isExist)
                {
                    dbContext.departments.Add(department);
                    dbContext.SaveChanges();
                }
            }


            List<string> roles = new List<string>()
            {
                "view",
                "edit",
                "create",
                "delete",
                "approval",
                "validator"
            };

            foreach (var role in roles)
            {
                bool isExist =   roleManager.Roles.Any(t => t.Name.Equals(role));
                if (!isExist)
                {
                    roleManager.CreateAsync(new IdentityRole(role));
                }
            }

        }
   
        // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();
       

        app.UseCors("MyPolicy");
        app.UseAuthentication();
        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        //CREATED DEFAULT DATA WHTCH SYSTEM SHOULD HAVE AT THE BEGINNING
        app.Run();
        }
}