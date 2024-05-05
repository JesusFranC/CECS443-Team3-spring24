using Microsoft.AspNetCore.Mvc;

namespace Team3.ThePollProject.Controllers
{
    [Route("[controller]")]
    public class RatingController : Controller
    {

        [HttpPost]
        [Route("postPoll")]
        public IActionResult postNewPoll()
        {
            return Ok();
        }
    }
}
