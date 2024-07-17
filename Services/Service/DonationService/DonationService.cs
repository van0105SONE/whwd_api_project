using ApplicationCore.Constanst;
using ApplicationCore.Dtos;
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
        public async Task<ErrorOr<MessageReponse<DonationResponseDto>>> createDonation([FromBody] DonationDto donationParam)
		{
			try
			{

			     Donation donation =	_Mapper.Map<Donation>(donationParam);
			     ApplicationUser? user =  await	_UserManager.FindByIdAsync(donationParam.userId);
			     Account account = await	_accountRepository.getAccountById(donationParam.accountId);
			    	Donator donator = new Donator();

				if (user == null)
				{
					return Error.Validation(ErrorCodes.Validation, "User id is invalid, user id is required");
				}else if (donationParam.Name == null)
				{
					return Error.Validation(ErrorCodes.Validation, "Account id is invalid, account is required");
				}

				Transaction transaction = new Transaction()
				{
					Account = account,
					Description = "Donate from donator",
					Amount = donationParam.amount,
					TransactionType = donation.DonationType,
					CreateBy = user
				};

				var trxResult = await _transacitonRepos.createTransaction(transaction);


				if (trxResult.IsError)
				{
                    return new MessageReponse<DonationResponseDto>()
                    {
                        isSuccess = false,
                        statusCode = 500,
                        message = "Can not create transactoin, unexpect system crash",
                    };
                }
                else
				{

                        donator = new Donator()
                        {
                            Name = donationParam.Name,
                            Facebook = donationParam.Facebook,
                            PhoneNumber = donationParam.PhoneNumber,
                            CreateBy = user
                        };
                        await _DonationRepository.creattDonator(donator);
                    

                    donation.DonorBy = donator;


                    var result = await _DonationRepository.createDonation(donation);

                    if (result.Value)
                    {
                        DonationResponseDto donationResponse = _Mapper.Map<DonationResponseDto>(donation);
                        return new MessageReponse<DonationResponseDto>()
                        {
                            isSuccess = false,
                            statusCode = 200,
                            message = "Successful",
                            data = donationResponse
                        };
                    }
                    else
                    {
                        return new MessageReponse<DonationResponseDto>()
                        {
                            isSuccess = false,
                            statusCode = 500,
                            message = "Fail to create donation data"
                        };

                    }
                }

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

        public async Task<ErrorOr<MessageReponse<DonationResponseDto>>> updateDonation(Guid Id,DonationDto donationParam)
		{
			try
			{

			    Donation donation = await	_DonationRepository.getDonationById(Id);

				if (donation == null)
				{
					return Error.Validation(ErrorCodes.Validation,"Donation not found");
				}

				donation.amount = donationParam.amount;
				donation.Title = donationParam.Title;

				ApplicationUser? user = await _UserManager.FindByIdAsync(donationParam.userId);
				donation.DonorBy.Name = donationParam.Name;
				donation.DonorBy.Facebook = donationParam.Facebook;
				donation.DonorBy.PhoneNumber = donationParam.PhoneNumber;
				donation.UpdateBy = user;
				

				var result = await _DonationRepository.updateDonation(donation);
                if (result.Value)
                {
                    DonationResponseDto donationResponse = _Mapper.Map<DonationResponseDto>(donation);
                    return new MessageReponse<DonationResponseDto>()
                    {
                        isSuccess = false,
                        statusCode = 200,
                        message = "Successful",
                        data = donationResponse
                    };
                }
                else
                {
                    return new MessageReponse<DonationResponseDto>()
                    {
                        isSuccess = false,
                        statusCode = 500,
                        message = "Fail to create donation data"
                    };

                }
            }
            catch(Exception ex)
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
