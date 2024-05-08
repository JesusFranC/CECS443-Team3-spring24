using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Team3.ThePollProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterationController : ControllerBase
    {

        // POST api/<RegisterationController>
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

    }
}
