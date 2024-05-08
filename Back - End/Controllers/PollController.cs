using Microsoft.AspNetCore.Mvc;
using System.Text.Json; // Import your model namespace
using Team3.ThePollProject.Model;
using Team3.ThePollProject.Models;
using Team3.ThePollProject.Models.Response;
using Team3.ThePollProject.SecurityLibrary.Interfaces;
using Team3.ThePollProject.Services;
using Team3ThePollProject.Security;

namespace Team3.ThePollProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PollController : ControllerBase
    {
        private readonly IPollingService _pollingService;
        private readonly ISecurityManager _securityManager;

        public PollController(IPollingService pollingService, ISecurityManager securityManager)
        {
            _pollingService = pollingService;
            _securityManager = securityManager;
        }

        // GET: api/Poll
        [HttpGet]
        public IActionResult GetPolls()
        {
            IResponse response;
            response = _pollingService.GetPolls();

            if (response.HasError == true)
            {
                return BadRequest();
            }
            else if (response.HasError == false)
            {
                var JsonRatings = JsonSerializer.Serialize(response.ReturnValue);

                return Ok(JsonRatings);
            }
            else
            {
                return Ok("No polls found");
            }
        }

        // GET: api/Poll/{id}
        [HttpGet("{id}")]
        public IActionResult GetPoll(long id)
        {
            IResponse response;
            response = _pollingService.GetPoll(id);

            if (response.HasError == true)
            {
                return BadRequest();
            }
            else if (response.HasError == false)
            {
                var JsonRatings = JsonSerializer.Serialize(response.ReturnValue);

                return Ok(JsonRatings);
            }
            else
            {
                return Ok("The specific rating found");
            }
        }

        // POST: api/Poll
        [HttpPost]
        public IActionResult CreatePoll(string title, string description, string[] pollOptions)
        {
            IResponse response;
            IAppPrincipal principal = _securityManager.JwtToPrincipal();
            IAccountUserModel user = new AccountUserModel(principal.userIdentity.userName);
            user.UserId = principal.userIdentity.UID;
            user.UserHash = principal.userIdentity.userHash;


            response = _pollingService.CreatePoll(user.UserId, title, description, pollOptions);

            if (response.HasError == true)
            {
                return BadRequest();
            }
            else if (response.HasError == false)
            {
                var JsonRatings = JsonSerializer.Serialize(response.ReturnValue);

                return Ok(JsonRatings);
            }
            else
            {
                return Ok("Poll has been created");
            }
        }

        // PUT: api/Poll/{id}
        [HttpPut("{id}")]
        public IActionResult UpdatePoll(long id, [FromBody] PollingModel poll)
        {
            // Implement logic to update an existing poll
            return NoContent();
        }

        // DELETE: api/Poll/{id}
        [HttpDelete("{id}")]
        public IActionResult DeletePoll(long id)
        {
            IResponse response;
            response = _pollingService.DeletePoll(id);

            if (response.HasError == true)
            {
                return BadRequest();
            }
            else if (response.HasError == false)
            {
                var JsonRatings = JsonSerializer.Serialize(response.ReturnValue);

                return Ok(JsonRatings);
            }
            else
            {
                return Ok("The specific poll found and deleted");
            }
        }
    }

}
