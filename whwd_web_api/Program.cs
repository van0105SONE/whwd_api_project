using Infrastructure.DataBaseContext;
using Infrastructure.Model;
using Infrastructure.Model.Volunteer;
using Infrastructure.Model.Work;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("MyPolicy");
        app.UseAuthentication();
        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        //CREATED DEFAULT DATA WHTCH SYSTEM SHOULD HAVE AT THE BEGINNING


        using (var scope = app.Services.CreateScope())
        {


            //add super user
            var _UserManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var _DbContext = scope.ServiceProvider.GetRequiredService<DatabaseContexts>();

            //departments
            List<Department> departments = new List<Department>() {
                 new Department()
                 {
                     Id = Guid.NewGuid(),
                     Name = "ມະຫາວິທະຍາໄລ່ແຫ່ງຊາດ"
                 },
                 new Department()
                 {
                     Id     = Guid.NewGuid(),
                     Name = "ມະຫາວິຍາໄລ່ສຸພານຸວົງ"
                 }
             };


            foreach(var department in  departments)
            {
                bool isExist = _DbContext.departments.Any(t => t.Name.ToLower() == department.Name.ToLower());
                if (!isExist)
                {
                    _DbContext.departments.Add(department);
                    _DbContext.SaveChanges();
                }
            }

         Department? noul =   _DbContext.departments.FirstOrDefault(t => t.Name == "ມະຫາວິທະຍາໄລ່ແຫ່ງຊາດ");

        if(noul != null)
            {
                List<Faculty> faculties = new List<Faculty>()
           {
               new Faculty()
               {
                   Id = Guid.NewGuid(),
                   Name = "ຄະນະວິທະຍາສາດທຳມະຊາດ",
                   Department = noul,
               },
               new Faculty()
               {
                   Id = Guid.NewGuid(),
                   Name = "ຄະນະອັກສອນສາດ",
                        Department = noul,
               },
               new Faculty()
               {
                   Id = Guid.NewGuid(),
                   Name = "ຄະນະເສດຖະສາດ",
                        Department = noul,
               }
              };

                foreach(var faculty in faculties)
                {
                    bool isExist = _DbContext.faculty.Any(t => t.Name == faculty.Name);
                    if (!isExist)
                    {
                        _DbContext.faculty.Add(faculty);
                        _DbContext.SaveChanges();
                    }

                }
 
           }

           Faculty? foul = _DbContext.faculty.FirstOrDefault(t => t.Name.Equals("ຄະນະວິທະຍາສາດທຳມະຊາດ"));


            if (foul != null)
            {
                Major major = new Major()
                {
                    Id = Guid.NewGuid(),
                    Name = "ການພັດທະນາໂປຣແກັມ",
                    Faculty = foul
                };
                bool isExist = _DbContext.majors.Any(t => t.Name == major.Name);
                if (!isExist)
                {
                    _DbContext.majors.Add(major);
                    _DbContext.SaveChanges();
                }

        }



            //Generations
            List<Generation> generations = new List<Generation>()
            {
                new Generation()
                {
                    ReferenceCode = "BORUM001",
                    number = 1,
                },
                new Generation()
                {
                    ReferenceCode = "BORUM002",
                    number = 2,
                },
                new Generation()
                {
                    ReferenceCode = "BORUM003",
                    number = 3,
                },
                new Generation()
                {
                    ReferenceCode = "BORUM004",
                    number = 4,
                },
                                new Generation()
                {
                    ReferenceCode = "BORUM005",
                    number = 5,
                }
            };


            foreach (var generation in generations)
            {
                bool isExist = _DbContext.generations.Any(t => t.ReferenceCode.Equals(generation.ReferenceCode));
                if (!isExist)
                {
                    _DbContext.generations.Add(generation);
                    _DbContext.SaveChanges();
                }

            }

            String UserName = "SuperAdmin99";
            ApplicationUser? isUserExist = _UserManager.FindByNameAsync(UserName).Result;

            Major? cpr =  _DbContext.majors.FirstOrDefault(t => t.Name.Equals("ການພັດທະນາໂປຣແກັມ"));
            Generation? genIII =  _DbContext.generations.FirstOrDefault(t => t.ReferenceCode.Equals("BORUM003"));


            if (isUserExist == null && cpr != null && genIII != null)
            {
                String password = "whwd2018@ADMIN";
                ApplicationUser superAdmin = new ApplicationUser()
                {
                    UserName = "SuperAdmin99",
                    Fname = "SuperAdmin",
                    Lname = "99",
                    Email = "whwdsuperadmin99@gmail.com",
                    Occupation = "Student",
                    Major = cpr,
                    Generation = genIII,
                };

                 var result =  _UserManager.CreateAsync(superAdmin, password).Result;
            }

            //departments
        }
        app.Run();
        }
}