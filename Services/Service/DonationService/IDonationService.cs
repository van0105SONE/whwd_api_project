using ApplicationCore.Dtos.Donate;
using ApplicationCore.Filter;
using ErrorOr;
using Infrastructure.Model.Account;
using Infrastructure.Model.Donate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.DonationService
{
	public interface IDonationService
	{
		public Task<ErrorOr<bool>> createDonation(DonationDto donationParam);
		public Task<ErrorOr<bool>> updateDonation(DonationUpdateDto donationParam);
		public Task<ErrorOr<bool>> deleteDonation(Guid Id);
		public Task<List<Donation>> getDonations(BaseFilter filter);
		public Task<Donation> getDonationById(Guid Id);
		public Task<List<SourceType>> getSourceTypes();
	}
}
