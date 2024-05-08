using Microsoft.AspNetCore.Mvc;
using System.Text.Json; // Import your model namespace
using Team3.ThePollProject.LoggingLibrary;
using Team3.ThePollProject.Models;
using Team3.ThePollProject.Models.Response;
using Team3.ThePollProject.Services;
using Team3ThePollProject.Security;

namespace Team3.ThePollProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PollController : ControllerBase
    {
        private readonly ILogService _logService;
        private readonly PollingService _pollingService;
        private readonly ISecurityManager _securityManager;

        public PollController(ILogService logService, PollingService pollingService, ISecurityManager securityManager)
        {
            _logService = logService;
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
                return Ok("No ratings found");
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
        public IActionResult CreatePoll([FromBody] PollingModel poll)
        {
            // Implement logic to create a new poll
            return CreatedAtAction(nameof(GetPoll), new { id = poll.PollID }, poll);
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
            // Implement logic to delete a poll by id
            return NoContent();
        }
    }
}
