using ApplicationCore.Filter;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Address;
using Infrastructure.Model.University;
using Infrastructure.Model.Users;
using Infrastructure.Repository.Implement;
using Infrastructure.Repository.IRepository;
using Infrastructure.Repository.UniversityRepos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.UserService
{
    public class UserService : IUserService
    {
        private UserManager<ApplicationUser> _UserManager { get; set; }
        private IUserRepository userRepository { get; set; }
        private IAddressRepository addressRepository { get; set; }
        private IUniversityRespository  universityRespository { get; set; }
       
       public UserService(UserManager<ApplicationUser> userManager, DatabaseContexts dbContext) { 
          _UserManager = userManager;
            addressRepository = new AddressRepository(dbContext);
            universityRespository = new UniversityRepository(dbContext);
            userRepository = new UserRepository(dbContext); 
        }
       async  public Task<bool> createUser(ApplicationUser user)
        {
            try
            {
                if (user.UserName == null)
                {
                    throw new Exception("Username isn't valid, username is required");
                }
                else if (user.Email == null){
                    throw new Exception("Email isn't valid, username is required");
                }


                if (String.IsNullOrEmpty(user.BornVillage.villageCode) ) {

                    var district = addressRepository.getDistrictById(user.BornVillage.villageCode);
                    Village village = new Village()
                    {
                        villageCode = Guid.NewGuid().ToString(),
                        villageName = user.BornVillage.villageName,
                        district = district,
                    };
                    var villageResult = addressRepository.createVillage(village);
                    user.BornVillage = village;
                }



                if (String.IsNullOrEmpty(user.CurrentVillage.villageCode))
                {
                    var district = addressRepository.getDistrictById(user.BornVillage.villageCode);
                    Village village = new Village()
                    {
                        villageCode = Guid.NewGuid().ToString(),
                        villageName = user.BornVillage.villageName,
                        district = district,
                    };

                    var villageResult = addressRepository.createVillage(village);
                    user.CurrentVillage = village;
                }


                if (String.IsNullOrEmpty(user.Major.Id))
                {

                    var department = universityRespository.getDepartmentById(user.Major.Department.Id);

                    var newMajor = new Major()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = user.Major.Name,
                        Department = department,
                    };


                    var major = universityRespository.createMajor(newMajor);
                    user.Major = major;
                }

                user.UserType = userRepository.getUserTypeById(user.UserType.Id);


                var result = await _UserManager.CreateAsync(user);
                return result.Succeeded;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool deleteUser(Guid Id)
        {
            throw new NotImplementedException();
        }

        public List<ApplicationUser> GetUsers(BaseFilter filter)
        {
            throw new NotImplementedException();
        }

        public bool updateUser(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public List<UserType> getUserTypes()
        {
            try
            {
              return  userRepository.getUserTypes();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
