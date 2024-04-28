using Infrastructure.Model.Work;
using Microsoft.AspNetCore.Mvc;

namespace whwd_web_api.Controllers.WorkController
{
    public class PlanningController : Controller
    {
        public PlanningController()
        {

        }


        [Route("/create-plan")]
        [HttpPost]
        public IActionResult CreateProjectPlan([FromBody] ProjectPlan requestBody)
        {
            try
            {
                return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);

            }

        }
    }
}
