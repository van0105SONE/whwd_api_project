using ApplicationCore.Constanst;
using ApplicationCore.Dtos.Donate;
using ApplicationCore.Filter;
using AutoMapper;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Account;
using Infrastructure.Model.Donate;
using Infrastructure.Model.Users;
using Infrastructure.Model.Work;
using Infrastructure.Repository.DonationRepostiory;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Services.Service.DonationService;
using System.Diagnostics.CodeAnalysis;
using whwd_web_api.Errors;


namespace whwd_web_api.Controllers.WorkController
{
	[ApiController]
	public class DonationController : Controller
	{

		private IDonationService _donationService { get; set; }
		private DatabaseContexts _databaseContexts { get; set; }
		public DonationController(UserManager<ApplicationUser> userManager, DatabaseContexts dbContext, IMapper mapper)
		{
			_databaseContexts = dbContext;
			_donationService = new DonationService(userManager, dbContext, mapper);
			
		}

		[HttpPost]
		[Route("createDonation")]
		public async Task<IActionResult> createDonation([FromBody] DonationDto donationDto)
		{
			try
			{
                var user = _databaseContexts.Users.FirstOrDefault();
				donationDto.userId = user.Id;
                var result =  await _donationService.createDonation(donationDto);




				if (result.IsError)
				{
					return Ok(ErrorHandler<DonationResponseDto>.HandleErrorResponse(result.FirstError.Code, result.FirstError.Description));
				}
				else
				{
					return Ok(result.Value);
				}
			}catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}


        [HttpPost]
        [Route("requestDonation")]
        public async Task<IActionResult> requestDonate([FromBody] DonationDto donationDto)
        {
            try
            {
				donationDto.DonationType = "UNKOWN";
                var result = await _donationService.createDonation(donationDto);
                if (result.IsError)
                {
                    return Ok(ErrorHandler<DonationResponseDto>.HandleErrorResponse(result.FirstError.Code, result.FirstError.Description));
                }
                else
                {
                    return Ok(result.Value);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut]
        [Route("approveOnlineDonate/{Id}")]
        public async Task<IActionResult> approveOnlineDonate(Guid Id)
        {
            try
            {
                Donation donation =   _databaseContexts.Donation.FirstOrDefault(t => t.Id == Id);
				donation.DonationType = "ບໍ່ປະສົງອອກນາມ";
		    	Account? account =	_databaseContexts.accounts.FirstOrDefault(t => t.AccountTypes.ToUpper() == "MAIN" && t.ProjectPlan.IsActive);
				account.DepositAmount += donation.amount;
				account.Balance += donation.amount;
                _databaseContexts.accounts.Update(account);
				ProjectPlan projectPlan = _databaseContexts.projectPlan.FirstOrDefault(t => t.IsActive);
				_databaseContexts.projectPlan.Update(projectPlan);
				_databaseContexts.SaveChanges();

				return Ok();

            }
            catch (Exception ex)
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
		[Route("updateDonation/{donationId}")]
		public async Task<IActionResult> updateDonaton(Guid donationId,[FromBody] DonationDto donationDto)
		{
			try
			{
				var result = await _donationService.updateDonation(donationId, donationDto);
				if (result.IsError)
				{
					return Ok(ErrorHandler<DonationResponseDto>.HandleErrorResponse(result.FirstError.Code, result.FirstError.Description));
				}
				else
				{
					return Ok(result.Value);
				}
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
		[Route("getDonationType")]
		public async Task<IActionResult> getSourceTypes()
		{
			try
			{
				return Ok(new List<string>()
				{
					"Online",
					"Offline",
					"Donation"
				});
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}
	}
}
