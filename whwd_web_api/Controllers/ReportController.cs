using ApplicationCore.Dtos;
using ApplicationCore.Dtos.Donate;
using ApplicationCore.Dtos.Recipient;
using ApplicationCore.Dtos.Reports;
using ApplicationCore.Dtos.TransactionDto;
using ApplicationCore.Filter.report;
using AutoMapper;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Account;
using Infrastructure.Model.Users;
using Infrastructure.Repository.DonationRepostiory;
using Infrastructure.Repository.StudentRepository;
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
        IStudentRepository _studentRepository { get; set; }
        IMapper _mapper { get; set; }
        public ReportController(DatabaseContexts dbContext, UserManager<ApplicationUser> userManager, IMapper mapper) {
            _transactionRepo = new TransactionRepository(dbContext);
            _donationRepos =  new DonationRepository(dbContext); 
            _studentRepository = new StudentRepository(dbContext);
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
                report.totalDonation = await _transactionRepo.getTotalDonation();
                report.totalTransaction = await _transactionRepo.getTotalTransaction();
                report.totalIncome = await _transactionRepo.getTotalIncome();
                report.totalExpense = await _transactionRepo.getTotalExpense();

                return Ok(report);
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("getDonationReport")]
        public async Task<IActionResult> getDonationReport([FromQuery] ReportAccountFilter filter)
        {
            try
            {
                DonationReport report = new DonationReport();
                var result = await _donationRepos.getDonationReports(filter);
                var response = _mapper.Map<List<DonationResponseDto>>(result);
                report.totalOffline = await _donationRepos.getTotalDonationWithSourceType("offline");
                report.totalOnline = await _donationRepos.getTotalDonationWithSourceType("online");
                report.totalThing = await _donationRepos.getTotalDonationWithDonationType("online");
                report.totalDonation = await _donationRepos.getTotalDonation();
                report.donations = response;
                return Ok(report);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpGet("getRecipientReport")]
        public async Task<IActionResult> getRecipientReport([FromQuery] RecipientReportFilter filter)
        {
            try
            {
            var totalRecipient =    await  _studentRepository.getTotalRecipient();
            var recipientBySchools =     await _studentRepository.getTotalRecipientBySchool();
            var responseBySchool =     _mapper.Map<List<RecipientBySchool>>(recipientBySchools);
            var recipientByReport =  await _studentRepository.getRecipientReport(filter);
            var response = _mapper.Map<List<RecipientReponseDto>>(recipientByReport);

                return Ok(new MessageReponse<RecipientReport>()
                {
                    data = new RecipientReport()
                    {
                        totalRecipient = totalRecipient,
                        totalRecipientBySchool = responseBySchool,
                        recipients = response
                    },
                });


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("getConjoint")]
        public IActionResult getConjointReport()
        {
            return NoContent();
        }
    }
}
