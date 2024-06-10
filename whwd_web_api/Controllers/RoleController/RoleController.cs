using ApplicationCore.Dtos;
using ApplicationCore.Dtos.Roles;
using AutoMapper;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Middleware;
using Services.Service.RoleSevice;

namespace whwd_web_api.Controllers.RoleController
{
    [ApiController]
    public class RoleController : Controller
    {

        private IMapper _mapper { get; set; }
        private RoleManager<IdentityRole> _roleManager { get; set; }
        private UserManager<ApplicationUser> _userManager { get; set; }
        private IRoleService roleService { get; set; }

        public RoleController(RoleManager<IdentityRole>  roleManager , UserManager<ApplicationUser> userManager, DatabaseContexts dbContext, IMapper mapper) { 
             _userManager = userManager;
             _mapper = mapper;
            _roleManager = roleManager;
            roleService = new RoleService( roleManager,userManager,dbContext, mapper);
     
        }

        [HttpGet]
        [Route("getRoles")]
        public IActionResult getRoles()
        {
            try
            {
               var roleResult =  roleService.getRoles();
               return  roleResult.Match(t => Ok(t), err => Problem(err.FirstOrDefault().Description));
            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("getRoleByUserId")]
        public async Task<IActionResult> getUsers([FromQuery] CheckRole userRole )
        {
            try
            {
                var result = await roleService.checkUserRole(userRole);

			  return Ok(result.Value);
            }catch( Exception ex )
            {
                return Problem(ex.Message);
            }
        }


        [HttpPost]
        [Route("addUserRole")]
        public async Task<IActionResult> addUserRole(UserRoleDto userRoleDto)
        {
            try
            {
                var result = await roleService.addUserRole(userRoleDto);
                return result.Match(t => Ok(t), err => Problem(err.FirstOrDefault().Description));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }        

        [HttpGet]
        [Route("getPositioin")]
        public IActionResult getPositions()
        {
            try
            {
              var positionResults  =    roleService.getPositions();
                return positionResults.Match(t => Ok(t), err => Problem(err.FirstOrDefault().Description));
            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("getTeams")]
        public IActionResult getTeams()
        {
            try
            {
                var positionResults = roleService.getProjectTeam();
                return positionResults.Match(t => Ok(t), err => Problem(err.FirstOrDefault().Description));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [Route("CreateRole")]
      async  public  Task<IActionResult> CreateRole(){
            try{
                            List<string> roles = new List<string>(){
                "create",
                "edit",
                "view",
                "approve"
            };
            
            foreach(var role in roles){
             await _roleManager.CreateAsync(new IdentityRole(role));
            }
            return Ok(new MessageReponse(){ isSuccess = true, message = "create role successful" });
            }catch(Exception ex){
                return Problem(ex.Message); 
            }
        }
    }
}
