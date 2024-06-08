using ApplicationCore.Filter;
using ErrorOr;
using Infrastructure.Model.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.ConjointRepository
{
	public interface IConjointRepository
	{
		public Task<ErrorOr<bool>> createConjoint(Conjoint conJointParam);
		public Task<ErrorOr<bool>> updateConjoint(Conjoint conJointParam);
		public Task<ErrorOr<bool>> deleteConjoint(Guid Id);

		public Task<List<Conjoint>> getConjoints(BaseFilter filter);

		public Task<Conjoint> getConjointById(Guid Id);
	}
}
