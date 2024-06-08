using ApplicationCore.Constanst;
using ApplicationCore.Dtos.Donate;
using ApplicationCore.Filter;
using AutoMapper;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Users;
using Infrastructure.Repository.DonationRepostiory;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Service.DonationService;


namespace whwd_web_api.Controllers.WorkController
{
	[ApiController]
	public class DonationController : Controller
	{

		private IDonationService _donationService { get; set; }
		public DonationController(UserManager<ApplicationUser> userManager, DatabaseContexts dbContext, IMapper mapper)
		{
			_donationService = new DonationService(userManager, dbContext, mapper);
			
		}

		[HttpPost]
		[Route("createDonation")]
		public async Task<IActionResult> createDonation([FromBody] DonationDto donationDto)
		{
			try
			{
			    var result =  await _donationService.createDonation(donationDto);
				return result.Match(t => CreatedAtAction(nameof(createDonation), t), err => Problem(err.FirstOrDefault().Description));
			}catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		[HttpGet]
		[Route("getSponsorTypes")]
		public async Task<IActionResult> getSponsorTypes()
		{
			return Ok(Constant.SPONSOR_TYPES);
		}

		[HttpPut]
		[Route("updateDonation")]
		public async Task<IActionResult> updateDonaton([FromBody] DonationUpdateDto donationDto)
		{
			try
			{
				var result = await _donationService.updateDonation(donationDto);
				return result.Match(t => CreatedAtAction(nameof(updateDonaton), t), err => Problem(err.FirstOrDefault().Description));
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		[HttpGet]
		[Route("getDonations")]
		public async Task<IActionResult> getDonations([FromQuery] BaseFilter filter )
		{
			try
			{
		     return 	Ok(await _donationService.getDonations(filter));
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		[HttpGet]
		[Route("getSourceType")]
		public async Task<IActionResult> getSourceTypes()
		{
			try
			{
				return Ok(await _donationService.getSourceTypes());
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}
	}
}
