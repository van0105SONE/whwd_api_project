using ApplicationCore.Dtos.ConjointDto;
using ApplicationCore.Filter;
using AutoMapper;
using ErrorOr;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Users;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Service.ConjointService;

namespace whwd_web_api.Controllers.WorkController
{
	public class ConjointController : Controller
	{
		private IConjointService _conjointService { get; set; }
          public ConjointController(UserManager<ApplicationUser> userManager, DatabaseContexts dbContext, IMapper mapper) {
			_conjointService = new ConjointService(userManager, dbContext, mapper);
		
		}

		[HttpPost]
		[Route("createConjoint")]
		public async Task<IActionResult> createConjoint([FromBody] ConjointDto conjointDto)
		{
			try
			{
			    var result = await	_conjointService.createConjoint(conjointDto);
				return result.Match(t => CreatedAtAction(nameof(createConjoint), t), err => Problem(err.FirstOrDefault().Description));
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		[HttpGet]
		[Route("getConjoints")]
		public async Task<IActionResult> getConjoint([FromQuery] BaseFilter filter)
		{
			try
			{
			    return Ok( _conjointService.getConjoints(filter));
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

	}
}
