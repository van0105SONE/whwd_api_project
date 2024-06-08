using ApplicationCore.Dtos.TransactionDto;
using ApplicationCore.Filter;
using AutoMapper;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Service.AddressService;
using Services.Service.TransactionService;
using Services.Service.UserService;

namespace whwd_web_api.Controllers.WorkController
{
	public class TransactionController : Controller
	{
		IMapper _Mapper { get; set; }
		private UserManager<ApplicationUser> _UserManager { get; set; }
	    private ITransactionService _transactionService { get; set; }
		public TransactionController(UserManager<ApplicationUser> userManager, DatabaseContexts dbContext, IMapper mapper)
		{
			_UserManager = userManager;
			_transactionService = new TransactionService(userManager, dbContext, mapper);
			_Mapper = mapper;
		}

		[HttpPost]
		[Route("createTransaction")]
		public async Task<IActionResult> createTransaction([FromBody] TransactionDto transactonDto)
		{
			try
			{
			     var result =  await	_transactionService.createTransaction(transactonDto);
				return result.Match(t => CreatedAtAction(nameof(createTransaction), t), err => Problem(err.FirstOrDefault().Description));
			}catch(Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		[HttpGet]
		[Route("getTransactions")]
		public async Task<IActionResult> getTransaction(BaseFilter filter)
		{
			try
			{
				return Ok(await _transactionService.getTransactions(filter));
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}

		}





		[HttpGet]
		[Route("getTransactionTypes")]
		public async Task<IActionResult> getTransactionTypes()
		{
			try
			{
				return Ok(await _transactionService.getTransactionTypes());
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}
	}
}
