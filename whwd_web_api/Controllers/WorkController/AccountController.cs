using ApplicationCore.Dtos;
using ApplicationCore.Filter;
using AutoMapper;
using ErrorOr;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Service.AccountService;
using whwd_web_api.Dtos.Accounts;

namespace whwd_web_api.Controllers.WorkController
{
    [ApiController]
    public class AccountController : Controller
    {
        IAccountService _accountService;
    public AccountController(UserManager<ApplicationUser> userManager, DatabaseContexts dbContext, IMapper mapper)
        {
            _accountService = new AccountService(dbContext, userManager,mapper);
        }

    [HttpPost]
    [Route("createAccount")]
    public async Task<IActionResult> createUser([FromBody] AccountDto accountDto){
         try{
           var result =  await  _accountService.createAccount(accountDto);
           return  result.Match(t => CreatedAtAction(nameof(createUser), new MessageReponse(){
            isSuccess = true,
            message = "Create Successful"
           }),err => Problem(err.FirstOrDefault().Description) );
         }catch(Exception ex){
            return Problem(ex.Message);
         }
    }

    [HttpPut]
    [Route("updateAccount")]
    public async Task<IActionResult> UpdateAccount([FromQuery] AccountUpdateDto accountDto){
      try{
          var result =  await _accountService.updateAccount(accountDto);
          return  result.Match(t => Ok(new MessageReponse(){
            isSuccess = t,
            message = "Update Successful"
          }), err => Problem(err.FirstOrDefault().Description));
      }catch(Exception ex){
        return Problem(ex.Message);
      }
    }
    [HttpDelete]
    [Route("deleteAccount")]
    public async Task<IActionResult> deleteAccount([FromQuery] Guid  Id ){
        try{
          var result = await _accountService.deleteAccount(Id);
          return result.Match(t => Ok(new MessageReponse(){
            isSuccess = true,
            message = "Delete Successful"
          } ), err => Problem(err.FirstOrDefault().Description));
        }catch(Exception ex){
          return Problem(ex.Message);
        }
    }

   [HttpGet]
   [Route("getAccounts")]
   public async Task<IActionResult> GetAccounts([FromQuery] BaseFilter filter){
      try{
        var result = await  _accountService.GetAllAccounts(filter);
        return Ok(result);
      }catch(Exception ex){
        return Problem(ex.Message);
      }
    }
  }
}
