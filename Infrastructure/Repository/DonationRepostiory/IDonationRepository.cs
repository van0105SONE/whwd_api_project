using ApplicationCore.Filter;
using ErrorOr;
using Infrastructure.Model.Account;
using Infrastructure.Model.Donate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.DonationRepostiory
{
	public interface IDonationRepository
	{
		public Task<ErrorOr<bool>> createDonation(Donation donationParam);
		public Task<ErrorOr<bool>> updateDonation(Donation donationParam);

		public Task<ErrorOr<bool>> creattDonator(Donator donatorParam);
		public Task<ErrorOr<bool>> updateDonator(Donator donatorParam);
		public Task<ErrorOr<bool>> deleteDonation(Guid Id);
		public Task<List<Donation>> getDonations(BaseFilter filter);
		public Task<Donation> getDonationById(Guid Id);
		public Task<Donator> getDonatorById(Guid Id);

		public Task<List<SourceType>> getSourceTypes();
		public Task<SourceType> getSourceTypeById(Guid Id);
		public Task<SourceType> getSourceTypeByName(string SourceName);
	}
}
