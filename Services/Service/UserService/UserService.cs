using ApplicationCore.Dtos.UserDto;
using ApplicationCore.Filter;
using AutoMapper;
using ErrorOr;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Address;
using Infrastructure.Model.University;
using Infrastructure.Model.Users;
using Infrastructure.Repository.Implement;
using Infrastructure.Repository.IRepository;
using Infrastructure.Repository.UniversityRepos;
using Microsoft.AspNetCore.Identity;
using Services.Service.RoleSevice;


namespace Services.Service.UserService
{
    public class UserService : IUserService
    {
        IMapper _mapper { get; set; }
        private UserManager<ApplicationUser> _UserManager { get; set; }
        IRoleService roleService { get; set; }

        private IUserRepository userRepository { get; set; }
        private IAddressRepository addressRepository { get; set; }
        private IUniversityRespository  universityRespository { get; set; }
       
       public UserService(UserManager<ApplicationUser> userManager, DatabaseContexts dbContext, IMapper mapper) { 
            _UserManager = userManager;
            roleService = new RoleService(userManager,dbContext, mapper);
            addressRepository = new AddressRepository(dbContext);
            universityRespository = new UniversityRepository(dbContext);
            userRepository = new UserRepository(dbContext); 
            _mapper = mapper;
        }
       async  public Task<ErrorOr<bool>> createUser(UserDto userDto)
        {
            try
            {


               ApplicationUser applicationUser =   _mapper.Map<ApplicationUser>(userDto);

                bool isUserValid = validateUser(userDto);
                if(!isUserValid){
                    return Error.Validation("Validation", "User information is't valid");
                }

                if (String.IsNullOrEmpty(userDto.BornVillage.villageCode) ) {
                    Village village = createVillageWithCodeNull(userDto.CurrentVillage.district.districtCode, userDto.CurrentVillage.villageName);
                    applicationUser.BornVillage = village;
                }



                if (String.IsNullOrEmpty(userDto.CurrentVillage.villageCode))
                {
                   Village village =   createVillageWithCodeNull(userDto.CurrentVillage.district.districtCode, userDto.CurrentVillage.villageName);
                    applicationUser.CurrentVillage = village;
                }


                if (String.IsNullOrEmpty(userDto.Major.Id))
                {
                    var department = universityRespository.getDepartmentById(userDto.Major.DepartmentId);

                    var newMajor = new Major()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = userDto.Major.Name,
                        Department = department,
                    };
                    var major = universityRespository.createMajor(newMajor);
                    applicationUser.Major = major;
                }

                applicationUser.UserType = userRepository.getUserTypeById(userDto.typeId);
                var result = await _UserManager.CreateAsync(applicationUser, userDto.Password);


                if (result.Succeeded)
                {
                    var roleResult = await roleService.addPosition(userDto.teamId, userDto.positionId, userDto.UserName);
                    return roleResult.Value;
                }
                else
                {
                    return Error.Failure("Failure", result.Errors.FirstOrDefault() == null  ? "Somthing went wrong" : result.Errors.FirstOrDefault().Description);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


      async  public  Task<ErrorOr<bool>> updateUser(UserUpdateDto userDto)
        {
            if (userDto is null)
            {
                throw new ArgumentNullException(nameof(userDto));
            }

            try
            {
                ApplicationUser userMapper = _mapper.Map<ApplicationUser>(userDto);

                ApplicationUser? userData = await _UserManager.FindByIdAsync(userDto.Id);
                if (userData == null)
                {
                    return Error.NotFound("ValidationError", "User isn't exist");
                }

                userData.Fname = userMapper.Fname;
                userData.Lname = userMapper.Lname;
                userData.UserName = userMapper.UserName;
                if (String.IsNullOrEmpty(userMapper.BornVillage.villageCode))
                {
                    Village village = createVillageWithCodeNull(userMapper.CurrentVillage.district.districtCode, userMapper.CurrentVillage.villageName);
                    userData.BornVillage = village;
                }



                if (String.IsNullOrEmpty(userMapper.CurrentVillage.villageCode))
                {
                    Village village = createVillageWithCodeNull(userMapper.CurrentVillage.district.districtCode, userMapper.CurrentVillage.villageName);
                    userData.CurrentVillage = village;
                }

          
                userData.PhoneNumber = userMapper.PhoneNumber;
                userData.Email = userMapper.Email;
                userData.UserType = userRepository.getUserTypeById(userMapper.Id);
                userData.Occupation = userMapper.Occupation;

                var updateResult = await _UserManager.UpdateAsync(userData);
                if (updateResult.Succeeded)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        async public Task<ErrorOr<bool>> deleteUser(Guid Id)
        {
            try
            {
                ApplicationUser? applicationUser = await _UserManager.FindByIdAsync(Id.ToString());
                if(applicationUser == null)
                {
                    return Error.NotFound("NotFound", "User isn't found");
                }

                var deleteResult = await   _UserManager.DeleteAsync(applicationUser);
                if (deleteResult.Succeeded)
                {
                    return true;
                }else
                {
                    return false;
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ApplicationUser> GetUsers(BaseFilter filter)
        {
            try{
                 return  userRepository.getUsers();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
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

        private Village createVillageWithCodeNull(string districtCode, string villageName)
        {
            try
            {
                var district = addressRepository.getDistrictById(districtCode);
                Village village = new Village()
                {
                    villageCode = Guid.NewGuid().ToString(),
                    villageName = villageName,
                    district = district,
                };
                var villageResult = addressRepository.createVillage(village);
                return village;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ApplicationUser> getUserById(string Id)
        {
            try
            {
                ApplicationUser? user =  userRepository.getUserById(Id);
                return user;
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


       private bool validateUser(UserDto userDto){
                    //auto mapper data to model

                if (userDto.UserName == null || userDto.Email == null)
                {
                    return false;
                }else if(String.IsNullOrEmpty(userDto.Fname) || String.IsNullOrEmpty(userDto.Lname)){
                    return false;
                }else if(String.IsNullOrEmpty(userDto.Occupation)){
                    return false;
                }
             return true;
       }
    }
}
