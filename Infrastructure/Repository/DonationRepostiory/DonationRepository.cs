using ApplicationCore.Filter;
using ErrorOr;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Account;
using Infrastructure.Model.Donate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.DonationRepostiory
{
	public class DonationRepository : IDonationRepository
	{

		private DatabaseContexts _dbContext { get; set; }

		public DonationRepository(DatabaseContexts context)
		{
			_dbContext = context;
		}
		public async Task<ErrorOr<bool>> createDonation(Donation donationParam)
		{
			try
			{
				_dbContext.Donation.Add(donationParam);
				_dbContext.SaveChanges();
				return true;
			}catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<ErrorOr<bool>> creattDonator(Donator donatorParam)
		{
			try
			{
				_dbContext.donators.Add(donatorParam);
				_dbContext.SaveChanges();
				return true;
			}
			catch(Exception ex) {
				throw new Exception(ex.Message);
			}

		}

		public async Task<ErrorOr<bool>> deleteDonation(Guid Id)
		{
			try
			{
			  Donation? donation =  	_dbContext.Donation.FirstOrDefault(t => t.Id == Id);
				_dbContext.Remove(donation);
				_dbContext.SaveChanges();
				return true;
			}catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<Donation> getDonationById(Guid Id)
		{
			try
			{
				return _dbContext.Donation.FirstOrDefault(t => t.Id == Id);
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<List<Donation>> getDonations(BaseFilter filter)
		{
			try
			{
			   return  _dbContext.Donation.Include(t => t.DonorBy).Include(t => t.SourceTypes).Skip((filter.page - 1) * filter.pageSize).Take(filter.pageSize).ToList();
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<Donator> getDonatorById(Guid Id)
		{
			try
			{
				return _dbContext.donators.FirstOrDefault(t => t.Id == Id);
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<ErrorOr<bool>> updateDonation(Donation donationParam)
		{
			try
			{
				_dbContext.Donation.Update(donationParam);
				_dbContext.SaveChanges();
				return true;
			}catch(Exception ex) {
				throw new Exception(ex.Message);
			}
		}

		public async Task<ErrorOr<bool>> updateDonator(Donator donatorParam)
		{
			try
			{
				 _dbContext.donators.Update(donatorParam);
				_dbContext.SaveChanges();
				return true;
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}


		public async Task<SourceType> getSourceTypeById(Guid Id)
		{
			try
			{
				return _dbContext.sourceTypes.FirstOrDefault(t => t.Id == Id);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<SourceType> getSourceTypeByName(string SourceName)
		{
			try
			{
				return _dbContext.sourceTypes.FirstOrDefault(t => t.Name.ToUpper() == SourceName.ToUpper());
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<List<SourceType>> getSourceTypes()
		{
			try
			{
				return _dbContext.sourceTypes.ToList();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
