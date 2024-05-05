using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Team3.ThePollProject.Models; // Import your poll model namespace

namespace Team3.ThePollProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollController : ControllerBase
    {
        // GET: api/Poll
        [HttpGet]
        public IActionResult GetPolls()
        {
            // Implement logic to fetch all polls
            return Ok();
        }

        // GET: api/Poll/{id}
        [HttpGet("{id}")]
        public IActionResult GetPoll(int id)
        {
            // Implement logic to fetch poll by id
            return Ok();
        }

        // POST: api/Poll
        [HttpPost]
        public IActionResult CreatePoll()
        {
            // Implement logic to create a new poll
            return Ok();
        }

        // PUT: api/Poll/{id}
        [HttpPut("{id}")]
        public IActionResult UpdatePoll()
        {
            // Implement logic to update an existing poll
            return NoContent();
        }

        // DELETE: api/Poll/{id}
        [HttpDelete("{id}")]
        public IActionResult DeletePoll(int id)
        {
            // Implement logic to delete a poll by id
            return NoContent();
        }
    }
}

