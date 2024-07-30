using ApplicationCore.Dtos;
using ApplicationCore.Dtos.ConjointDto;
using ApplicationCore.Filter;
using AutoMapper;
using ErrorOr;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Users;
using Infrastructure.Model.Work;
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
		[Route("isJoint")]
        public async Task<IActionResult> isJoinst([FromQuery] Guid userId, Guid placeId)
        {
            try
             {
                bool isExist =  _dbContexts.conjoints.Any(t => t.Joiner.Id == userId.ToString() && placeId == t.FundRaisingPlace.Id);
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
        [Route("getJointByPlace")]
        public async Task<IActionResult> getConjoint([FromQuery] ContJointPlace filter)
        {
            try
            {
              var conjionts =  _dbContexts.conjoints.Include(t => t.CreateBy).Include(t => t.Joiner).Include(t => t.FundRaisingPlace).Where(t => t.CreateBy.Id == filter.userId.ToString() && t.FundRaisingPlace.Id == filter.placeId).Skip((filter.page -1) * filter.pageSize).Take(filter.pageSize).ToList();
                return Ok(conjionts);
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

        [HttpPut]
        [Route("updateJointStatus/{Id}")]
        public async Task<IActionResult> getTrackConjoint(Guid Id)
        {
            try
            {
                 Conjoint joint =   _dbContexts.conjoints.FirstOrDefault(t => t.Id == Id);
                 joint.JointFundRaising = true;

                _dbContexts.conjoints.Update(joint);
                _dbContexts.SaveChanges();
                return Ok(new MessageReponse<string>() { 
                  statusCode = 200,
                  message= "Success",
                  isSuccess = true

                });

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


    }
}
