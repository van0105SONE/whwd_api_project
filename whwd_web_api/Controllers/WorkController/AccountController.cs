using ApplicationCore.Dtos;
using ApplicationCore.Dtos.Accounts;
using ApplicationCore.Filter;
using AutoMapper;
using ErrorOr;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Account;
using Infrastructure.Model.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Service.AccountService;
using whwd_web_api.Dtos.Accounts;
using whwd_web_api.Errors;

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


                if (result.IsError)
                {
                    return Ok(ErrorHandler<AccountResponseDto>.HandleErrorResponse(result.Errors.FirstOrDefault().Code, result.Errors.FirstOrDefault().Description));
                }
                else
                {
                    return Ok(result.Value);
                }
            }
            catch(Exception ex){
            return Problem(ex.Message);
         }
    }

    [HttpPut]
    [Route("updateAccount/{accId}")]
    public async Task<IActionResult> UpdateAccount(Guid accId, [FromBody] AccountDto accountDto){
      try{
          var result =  await _accountService.updateAccount(accId,accountDto);

                if (result.IsError)
                {
                    return Ok(ErrorHandler<AccountResponseDto>.HandleErrorResponse(result.Errors.FirstOrDefault().Code, result.Errors.FirstOrDefault().Description));
                }
                else
                {
                    return Ok(result.Value);
                }
      }catch(Exception ex){
        return Problem(ex.Message);
      }
    }
    [HttpDelete]
    [Route("deleteAccount")]
    public async Task<IActionResult> deleteAccount([FromQuery] Guid  Id ){
        try{
          var result = await _accountService.deleteAccount(Id);
          return result.Match(t => Ok(new MessageReponse<Account>(){
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

    [HttpGet]
    [Route("getAccountTypes")]
    public async Task<IActionResult> getAccountTypes()
        {
            try
            {
              return Ok(new List<String>()
              {
                  "Joint",
                  "Personal",
                  "Hand"
              });
            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }
  }
}
