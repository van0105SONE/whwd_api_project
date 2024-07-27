using ApplicationCore.Dtos.ConjointDto;
using ApplicationCore.Filter;
using AutoMapper;
using ErrorOr;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Users;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Service.ConjointService;

namespace whwd_web_api.Controllers.WorkController
{
	public class ConjointController : Controller
	{
		private DatabaseContexts _dbContexts { get; set; }
		private IConjointService _conjointService { get; set; }
          public ConjointController(UserManager<ApplicationUser> userManager, DatabaseContexts dbContext, IMapper mapper) {
			_conjointService = new ConjointService(userManager, dbContext, mapper);
			_dbContexts = dbContext;
		
		}


		[HttpGet]
		[Route("isJoint/{Id}")]
        public async Task<IActionResult> isJoinst(Guid Id)
        {
            try
            {
                bool isExist =  _dbContexts.conjoints.Any(t => t.Joiner.Id == Id.ToString());
				return Ok(isExist);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
		[Route("createConjoint")]
		public async Task<IActionResult> createConjoint([FromBody] ConjointDto conjointDto)
		{
			try
			{
			    var result = await	_conjointService.createConjoint(conjointDto);
				return result.Match(t => CreatedAtAction(nameof(createConjoint), t), err => Problem(err.FirstOrDefault().Description));
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}



		[HttpGet]
		[Route("getConjoints")]
		public async Task<IActionResult> getConjoint([FromQuery] BaseFilter filter)
		{
			try
			{
			    return Ok( await _conjointService.getConjoints(filter));
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}



        [HttpGet]
        [Route("getTrackConjoint")]
        public async Task<IActionResult> getTrackConjoint([FromQuery] ContJointFilter filter)
        {
            try
            {
				var conJoints = _dbContexts.conjoints.Include(t => t.FundRaisingPlace).Include(t => t.Joiner).Where(t => t.CreateAt.Date == filter.filterDate.Value.Date);
                return Ok(conJoints);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


    }
}
