using ApplicationCore.Dtos;
using ApplicationCore.Dtos.Roles;
using AutoMapper;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Services.Service.RoleSevice;

namespace whwd_web_api.Controllers.RoleController
{
    [ApiController]
    public class RoleController : Controller
    {

        private IMapper _mapper { get; set; }
        private RoleManager<ApplicationRoles> _roleManager { get; set; }
        private UserManager<ApplicationUser> _userManager { get; set; }
        private IRoleService roleService { get; set; }

        public RoleController(RoleManager<ApplicationRoles>  roleManager , UserManager<ApplicationUser> userManager, DatabaseContexts dbContext, IMapper mapper) { 
             _userManager = userManager;
             _mapper = mapper;
            _roleManager = roleManager;
            roleService = new RoleService( roleManager,userManager,dbContext, mapper);
     
        }


        [HttpPost]
        [Route("createRole")]
        public async Task<IActionResult> getRoles(string roleName)
        {
            try
            {
                var roleResult = await roleService.createRole(roleName);
                var jsonString = JsonConvert.SerializeObject(roleResult.Value, Formatting.Indented);
                return Ok(jsonString);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("getRoles")]
        public async Task<IActionResult> getRoles()
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    Formatting = Formatting.Indented
                };
                var roleResult =  roleService.getUserRoles();
                var jsonString = JsonConvert.SerializeObject(roleResult.Value, settings );
                
                return Ok(jsonString);

               
            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [Route("createRoleAccess")]
        public async Task<IActionResult> createRoleAccess([FromBody] RoleAccessDto roleAccessDto)
        {
            try
            {
                var roleResult = await roleService.createRoleAccess(roleAccessDto);
                var jsonString = JsonConvert.SerializeObject(roleResult.Value, Formatting.Indented);
                return Ok(jsonString);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("getRoleAccessByRoleId")]
        public async Task<IActionResult> getRoleAccess([FromQuery] Guid Id)
        {
            try
            {
                var roleAccessResult = await roleService.getRoleAccesses(Id);
                return Ok(roleAccessResult.Value);
            }
            catch (Exception ex)
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
               // var result = await roleService.get(userRole);
			    return Ok();
            }catch( Exception ex )
            {
                return Problem(ex.Message);
            }
        }


     

        [HttpGet]
        [Route("getPosition")]
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

    }
}
