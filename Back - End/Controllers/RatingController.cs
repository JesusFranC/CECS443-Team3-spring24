using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Team3.ThePollProject.Models; // Import your rating model namespace

namespace Team3.ThePollProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        // GET: api/Rating
        [HttpGet]
        [Route(/)]
        public IActionResult GetRatings()
        {
            // Implement logic to fetch all ratings
            return Ok();
        }

        // GET: api/Rating/{id}
        [HttpGet("{id}")]
        public IActionResult GetRating(int id)
        {
            // Implement logic to fetch rating by id
            return Ok();
        }

        // POST: api/Rating
        [HttpPost]
        public IActionResult CreateRating()
        {
            // Implement logic to create a new rating
            return Ok();
        }

        // PUT: api/Rating/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateRating()
        {
            // Implement logic to update an existing rating
            return NoContent();
        }
/
        // DELETE: api/Rating/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteRating(int id)
        {
            // Implement logic to delete a rating by id
            return NoContent();
        }
    }
}
