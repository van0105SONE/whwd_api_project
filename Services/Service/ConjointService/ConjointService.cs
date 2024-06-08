using ApplicationCore.Dtos.ConjointDto;
using ApplicationCore.Filter;
using AutoMapper;
using ErrorOr;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Place;
using Infrastructure.Model.Users;
using Infrastructure.Model.Work;
using Infrastructure.Repository.ConjointRepository;
using Infrastructure.Repository.FundRaisingPlaceRepos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.ConjointService
{
	public class ConjointService : IConjointService
	{
		IMapper _Mapper { get; set; }
		UserManager<ApplicationUser> _UserManager { get; set; }

		IConjointRepository _conjointService { get; set; }
		IFundRaisingPlaceRepos _fundRaisingPlaceRepos { get; set; }

		public ConjointService(UserManager<ApplicationUser> userManager, DatabaseContexts contexts, IMapper mapper) { 
		  
			_UserManager = userManager;
			_Mapper = mapper;
			_conjointService = new ConjointRepository(contexts);
			_fundRaisingPlaceRepos = new FundRaisingPlaceRepos(contexts);
		}
		public async Task<ErrorOr<bool>> createConjoint(ConjointDto conJointParam)
		{
			try
			{
			     Conjoint conjoint = _Mapper.Map<Conjoint>(conJointParam);

				ApplicationUser? user = await _UserManager.FindByIdAsync(conJointParam.UserId);
				if (user == null)
				{
					return Error.NotFound("User is required, please check user id");
				}
				ApplicationUser? joiner = await _UserManager.FindByIdAsync(conJointParam.JoinerId);

				if (joiner == null)
				{
					return Error.NotFound("Joiner is required, please check joiner id");
				}

			    FundRaisingPlace place = await	_fundRaisingPlaceRepos.getPlaceById(conJointParam.FundRaisingPlaceId);
				if (place == null)
				{
					return Error.NotFound("Place is not found in system, please try again");
				}

				conjoint.CreateBy = user;
				conjoint.Joiner = joiner;
				conjoint.FundRaisingPlace = place;
			    return await 	_conjointService.createConjoint(conjoint);
				
			}catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public Task<ErrorOr<bool>> deleteConjoint(Guid Id)
		{
			throw new NotImplementedException();
		}

		public Task<Conjoint> getConjointById(Guid Id)
		{
			try
			{
			  return	_conjointService.getConjointById(Id);
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public Task<List<Conjoint>> getConjoints(BaseFilter filter)
		{
			try
			{
				 return _conjointService.getConjoints(filter);
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public Task<ErrorOr<bool>> updateConjoint(ConjointUpdateDto conJointParam)
		{
			throw new NotImplementedException();
		}
	}
}
