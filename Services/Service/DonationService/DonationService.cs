using ApplicationCore.Dtos.Donate;
using ApplicationCore.Filter;
using AutoMapper;
using ErrorOr;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Account;
using Infrastructure.Model.Donate;
using Infrastructure.Model.Users;
using Infrastructure.Repository.AccountRepos;
using Infrastructure.Repository.DonationRepostiory;
using Infrastructure.Repository.TransactionRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Services.Service.DonationService
{
	public class DonationService : IDonationService

	{
		IMapper _Mapper;
		private UserManager<ApplicationUser> _UserManager { get; set; }
		private IDonationRepository _DonationRepository { get; set; }
		private IAccountRepository _accountRepository { get; set; }
		private ITransactionRepos _transacitonRepos { get; set; }

		public DonationService(UserManager<ApplicationUser> userManager, DatabaseContexts contexts, IMapper mapper)
		{
			_Mapper = mapper;
			_UserManager = userManager;
			_DonationRepository = new DonationRepository(contexts);
			_accountRepository = new AccountRepository(contexts);
			_transacitonRepos = new TransactionRepository(contexts);

		}
		public async Task<ErrorOr<bool>> createDonation([FromBody] DonationDto donationParam)
		{
			try
			{

			     Donation donation =	_Mapper.Map<Donation>(donationParam);
			     ApplicationUser? user =  await	_UserManager.FindByIdAsync(donationParam.userId);
			     Donator donator = await	_DonationRepository.getDonatorById(donationParam.DonatorId);
			     Account account = await	_accountRepository.getAccountById(donationParam.accountId);
				SourceType sourceType = await _DonationRepository.getSourceTypeById(donationParam.sourceTypeId);
				TransactionType trxtypes = await _transacitonRepos.getTransactionTypeByName("Donation");
				Transaction transaction = new Transaction()
				{
					Account = account,
					Description = "Donate from donator",
					Amount = donationParam.amount,
					TransactionType = trxtypes,
					CreateBy = user
				};

				if (donator == null)
				{

				  donator = new Donator()
					{
						Name = donationParam.Name,
						Facebook = donationParam.Facebook,
						PhoneNumber = donationParam.PhoneNumber,
						CreateBy = user
					};
					await _DonationRepository.creattDonator(donator);
				}
				donation.SourceTypes = sourceType; 
				donation.DonorBy = donator;
			  return await	_DonationRepository.createDonation(donation);
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<ErrorOr<bool>> deleteDonation(Guid Id)
		{
			try
			{
				return await _DonationRepository.deleteDonation(Id);
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<Donation> getDonationById(Guid Id)
		{
			try
			{
			   return await	_DonationRepository.getDonationById(Id);
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<List<Donation>> getDonations(BaseFilter filter)
		{
			try
			{
			    return await _DonationRepository.getDonations(filter);
			}catch(Exception ex) {
				throw new Exception(ex.Message);
			}
		}

		public async Task<ErrorOr<bool>> updateDonation(DonationUpdateDto donationParam)
		{
			try
			{

			    Donation donation = await	_DonationRepository.getDonationById(donationParam.Id);

				if (donation == null)
				{
					return Error.NotFound("Donation not found");
				}

				donation.Description = donationParam.Description;
				donation.amount = donationParam.amount;
				donation.Title = donationParam.Title;

				ApplicationUser? user = await _UserManager.FindByIdAsync(donationParam.userId);
				Donator donator = await _DonationRepository.getDonatorById(donationParam.DonatorId);
				donator = new Donator()
				{
					Name = donationParam.Name,
					Facebook = donationParam.Facebook,
					PhoneNumber = donationParam.PhoneNumber,
					UpdateBy = user,
					UpdateAt = DateTime.UtcNow
				};

				return await _DonationRepository.updateDonation(donation);
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}


		public async Task<List<SourceType>> getSourceTypes()
		{
			try
			{
				return await _DonationRepository.getSourceTypes();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
