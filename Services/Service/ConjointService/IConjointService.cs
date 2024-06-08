using ApplicationCore.Dtos.ConjointDto;
using ApplicationCore.Filter;
using ErrorOr;
using Infrastructure.Model.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.ConjointService
{
	public interface IConjointService
	{
		public Task<ErrorOr<bool>> createConjoint(ConjointDto conJointParam);
		public Task<ErrorOr<bool>> updateConjoint(ConjointUpdateDto conJointParam);
		public Task<ErrorOr<bool>> deleteConjoint(Guid Id);

		public Task<List<Conjoint>> getConjoints(BaseFilter filter);

		public Task<Conjoint> getConjointById(Guid Id);
	}
}
