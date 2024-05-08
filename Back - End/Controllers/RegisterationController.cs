using Microsoft.AspNetCore.Mvc;
using Team3.ThePollProject.Model;
using Team3.ThePollProject.Models.Response;
using Team3.ThePollProject.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Team3.ThePollProject.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegisterationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegisterationController(IRegistrationService registrationService) 
        { 
            _registrationService = registrationService;
        }

        // POST api/<RegisterationController>
        [HttpPost]
        [Route("Register")]
        public IActionResult Post(string email)
        {
            IResponse response = new Response();

            response = _registrationService.MakeUser(email);

            if(response.HasError)
            {
                return BadRequest();
            }

            return Ok();
        }

    }
}
