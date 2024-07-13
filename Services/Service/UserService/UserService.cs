using ApplicationCore.Constanst;
using ApplicationCore.Dtos;
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
using Infrastructure.Repository.RoleRepository;
using Infrastructure.Repository.UniversityRepos;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IRoleRepository _roleRepository;

        public UserService(UserManager<ApplicationUser> userManager, DatabaseContexts dbContext, IMapper mapper) { 
            _UserManager = userManager;
            roleService = new RoleService(userManager,dbContext, mapper);
            addressRepository = new AddressRepository(dbContext);
            universityRespository = new UniversityRepository(dbContext);
            userRepository = new UserRepository(dbContext);
            _roleRepository = new RoleRepository(dbContext);
            _mapper = mapper;
        }
       async  public Task<ErrorOr<MessageReponse<UserReponseDto>>> createUser(UserDto userDto)
        {
            try
            {
                ApplicationUser applicationUser = _mapper.Map<ApplicationUser>(userDto);

                bool isUserValid = validateUser(userDto);
                District bornDistrict = addressRepository.getDistrictById(userDto.BornVillage.district.districtCode);
                District currentDistrict = addressRepository.getDistrictById(userDto.CurrentVillage.district.districtCode);
                ApplicationRoles role = _roleRepository.getRoleById(userDto.RoleId);
                Village bornVillage = addressRepository.getVillageById(userDto.BornVillage.villageCode);
                Village currentVillage = addressRepository.getVillageById(userDto.CurrentVillage.villageCode);
                Major major = universityRespository.getMajorById(userDto.Major.Id);
                var department = universityRespository.getDepartmentById(userDto.Major.DepartmentId);


                if (!isUserValid)
                {
                    return Error.Validation(ErrorCodes.Validation, "User information is't valid");
                }
                if (role == null)
                {
                    return Error.Validation(ErrorCodes.Validation, "User Role isn't found in the system");
                }
                if (department == null)
                {
                    return Error.Validation(ErrorCodes.Validation, "Department isn't found in the system");
                }
                if (bornDistrict == null)
                {
                    return Error.Validation(ErrorCodes.Validation, "District isn't found in the system");
                }
                if (currentDistrict == null)
                {
                    return Error.Validation(ErrorCodes.Validation, "District isn't found in the system");
                }


                if (bornVillage == null)
                {
                    Village village = createVillageWithCodeNull(userDto.CurrentVillage.district.districtCode, userDto.CurrentVillage.villageName);
                    applicationUser.BornVillage = village;
                }
                if (currentVillage == null)
                {
                    Village village = createVillageWithCodeNull(userDto.CurrentVillage.district.districtCode, userDto.CurrentVillage.villageName);
                    applicationUser.CurrentVillage = village;
                }
                if (major == null)
                {
                    var newMajor = new Major()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = userDto.Major.Name,
                        Department = department,
                    };
                    var majorCreate = universityRespository.createMajor(newMajor);
                    applicationUser.Major = majorCreate;
                }

                applicationUser.Role = role;
                var result = await _UserManager.CreateAsync(applicationUser, userDto.Password);

                if (result.Succeeded)
                {

                    UserReponseDto userResponse = _mapper.Map<UserReponseDto>(applicationUser);
                    return new MessageReponse<UserReponseDto>()
                    {
                        isSuccess = true,
                        message = "Successful",
                        data = userResponse
                    };
                }
                else
                {
                    return Error.Validation(ErrorCodes.InternalError, result.Errors.FirstOrDefault().Description);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


      async  public Task<ErrorOr<MessageReponse<UserReponseDto>>> updateUser(string userId, UserDto userDto)
        {
            try
            {
                ApplicationUser userMapper = _mapper.Map<ApplicationUser>(userDto);
                Village bornVillage = addressRepository.getVillageById(userDto.BornVillage.villageCode);
                Village currentVillage = addressRepository.getVillageById(userDto.CurrentVillage.villageCode);
                ApplicationUser? userData = await _UserManager.FindByIdAsync(userId);
                Major major = universityRespository.getMajorById(userDto.Major.Id);
                var department = universityRespository.getDepartmentById(userDto.Major.DepartmentId);

                if (userData == null)
                {
                    return Error.Validation(ErrorCodes.NotFound, "User Id is invalid, User is required");
                }else if (major == null)
                {
                    return Error.Validation(ErrorCodes.Validation, "Major Id is invalid, Major is required");
                }


                if (bornVillage == null)
                {
                    Village village = createVillageWithCodeNull(userMapper.CurrentVillage.district.districtCode, userMapper.CurrentVillage.villageName);
                    bornVillage = village;
                }
                if (currentVillage == null)
                {
                    Village village = createVillageWithCodeNull(userMapper.CurrentVillage.district.districtCode, userMapper.CurrentVillage.villageName);
                    currentVillage = village;
                }

                userData.Fname = userMapper.Fname;
                userData.Lname = userMapper.Lname;
                userData.UserName = userMapper.UserName;
                userData.PhoneNumber = userMapper.PhoneNumber;
                userData.Email = userMapper.Email;
                userData.BornVillage = bornVillage;
                userData.CurrentVillage = currentVillage;
                userData.Occupation = userMapper.Occupation;
                var updateResult = await _UserManager.UpdateAsync(userData);

                if (updateResult.Succeeded)
                {
                    UserReponseDto userResponse = _mapper.Map<UserReponseDto>(userData);
                    return new MessageReponse<UserReponseDto>()
                    {
                        isSuccess = true,
                        message = "Successful",
                        data = userResponse
                    };
                }
                else
                {
                    return new MessageReponse<UserReponseDto>()
                    {
                        isSuccess = false,
                        message = updateResult.Errors.First().Description,
                    };
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


                var deleteResult = await _UserManager.DeleteAsync(applicationUser);
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

        public List<UserReponseDto> GetUsers(BaseFilter filter)
        {
            try{

               List<ApplicationUser> users =  userRepository.getUsers(filter);
               List<UserReponseDto> userResponse = _mapper.Map<List<UserReponseDto>>(users);

                return userResponse;
            }catch(Exception ex)
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
