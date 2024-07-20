using ApplicationCore.Dtos.Reports;
using ApplicationCore.Dtos.TransactionDto;
using ApplicationCore.Filter.report;
using AutoMapper;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Account;
using Infrastructure.Model.Users;
using Infrastructure.Repository.DonationRepostiory;
using Infrastructure.Repository.TransactionRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Service.PositionService;

namespace whwd_web_api.Controllers
{
    [ApiController]
    public class ReportController : Controller
    {
        ITransactionRepos _transactionRepo { get; set; }
        IDonationRepository _donationRepos { get; set; }
        IMapper _mapper { get; set; }
        public ReportController(DatabaseContexts dbContext, UserManager<ApplicationUser> userManager, IMapper mapper) {
            _transactionRepo = new TransactionRepository(dbContext);
            _donationRepos =  new DonationRepository(dbContext);    
            _mapper = mapper;
        }


        [HttpGet("GetAccountReport")]
        public async Task<IActionResult> getAccountReport([FromQuery] ReportAccountFilter filter)
        {
            try
            {
                AccountRepoortDto report = new AccountRepoortDto();
                var result = await _transactionRepo.getTransactions(filter);
               var response =    _mapper.Map<List<TransactionResponseDto>>(result);
                report.transactions = response;
                return NoContent();
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("getDonationReport")]
        public IActionResult getReport()
        {
            try
            {
/*                DonationReport report = new AccountRepoortDto();
                var result = await _transactionRepo.getTransactions(filter);
                var response = _mapper.Map<List<TransactionResponseDto>>(result);
                report.transactions = response;*/
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpGet("getRecipientReport")]
        public IActionResult getRecipientReport()
        {
            return NoContent();
        }

        [HttpGet("getConjoint")]
        public IActionResult getConjointReport()
        {
            return NoContent();
        }
    }
}
