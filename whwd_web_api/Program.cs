using Infrastructure.DataBaseContext;
using Infrastructure.Model.Account;
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

       using(var scope = app.Services.CreateScope()){
        var db = scope.ServiceProvider.GetRequiredService<DatabaseContexts>();

        List<AccountType> items = new List<AccountType>(){
            new AccountType(){
                Id = Guid.NewGuid(),
                Name = "ບັນຊີຮ່ວມ"
            },
            new AccountType(){
                Id = Guid.NewGuid(),
                Name = "ບັນຊີສ່ວນບຸກຄົນ"
            },
        };

        foreach(var item in items){
          db.accountTypes.Add(item);
          db.SaveChanges();
        }


			List<SourceType> sourceTypes = new List<SourceType>(){
			new SourceType(){
				Id = Guid.NewGuid(),
				Name = "Online"
			},
			new SourceType(){
				Id = Guid.NewGuid(),
				Name = "Offline"
			}
		};

			foreach (var item in sourceTypes)
			{
				db.sourceTypes.Add(item);
				db.SaveChanges();
			}



			List<TransactionType> trxTypes = new List<TransactionType>(){
			new TransactionType(){
				Id = Guid.NewGuid(),
				Name = "Donation"
			},
			new TransactionType(){
				Id = Guid.NewGuid(),
				Name = "Tranfer"
			},
		    new TransactionType(){
				Id = Guid.NewGuid(),
				Name = "Withdraw"
			},

		};

			foreach (var item in trxTypes)
			{
				db.transactionTypes.Add(item);
				db.SaveChanges();
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