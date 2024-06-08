using ApplicationCore.Filter;
using ErrorOr;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.ConjointRepository
{
	public class ConjointRepository : IConjointRepository
	{
		private DatabaseContexts _dbContext { get; set; }
		public ConjointRepository(DatabaseContexts context) {
		 _dbContext = context;
		}

		public async Task<ErrorOr<bool>> createConjoint(Conjoint conJointParam)
		{
			try
			{
				_dbContext.conjoints.Add(conJointParam);
				_dbContext.SaveChanges();
				 return true;
			}catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<ErrorOr<bool>> updateConjoint(Conjoint conJointParam)
		{
			try
			{
				_dbContext.conjoints.Update(conJointParam);
				_dbContext.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<ErrorOr<bool>> deleteConjoint(Guid Id)
		{
			try
			{
			     Conjoint conjoint =	_dbContext.conjoints.FirstOrDefault(t => t.Id == Id);
				_dbContext.Remove(conjoint);
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<List<Conjoint>> getConjoints(BaseFilter filter)
		{
			try
			{
				return _dbContext.conjoints.Skip((filter.page - 1) * filter.pageSize).Take(filter.pageSize).ToList();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<Conjoint> getConjointById(Guid Id)
		{
			try
			{
				Conjoint conjoint = _dbContext.conjoints.FirstOrDefault(t => t.Id == Id);
				return conjoint;
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
