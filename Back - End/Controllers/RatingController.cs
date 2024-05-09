using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Team3.ThePollProject.LoggingLibrary;
using Team3.ThePollProject.Model;
using Team3.ThePollProject.Models;
using Team3.ThePollProject.Models.Response;
using Team3.ThePollProject.SecurityLibrary.Interfaces;
using Team3.ThePollProject.Services;
using Team3ThePollProject.Security; // Import your rating model namespace

namespace Team3.ThePollProject.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class RatingController : ControllerBase
    {
        private readonly ILogService _logService;
        private readonly IRatingService _ratingService;
        private readonly ISecurityManager _securityManager;


        public RatingController(ILogService logService, IRatingService ratingService, ISecurityManager securityManager)
        {
            _logService = logService;
            _ratingService = ratingService;
            _securityManager = securityManager;
        }

        // GET: api/Rating
        [HttpGet]
        [Route("RetrieveRatings")]
        public IActionResult GetRatings()
        {
            // Implement logic to fetch all ratings
            IResponse response;
            //IAppPrincipal principal = _securityManager.JwtToPrincipal();
            //IAccountUserModel user = new AccountUserModel(principal.userIdentity.userName);
            //user.UserId = principal.userIdentity.UID;
            //user.UserHash = principal.userIdentity.userHash;

            response = _ratingService.GetRatings();

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

        [HttpGet]
        public IActionResult GetEntities()
        {
            // Implement logic to fetch all ratings
            IResponse response;


            response = _ratingService.GetEntities();

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

        // GET: api/Rating/{id}
        [HttpGet("{id}")]
        public IActionResult GetRating(int id)
        {
            // Implement logic to fetch rating by id
            IResponse response;
            IAppPrincipal principal = _securityManager.JwtToPrincipal();
            IAccountUserModel user = new AccountUserModel(principal.userIdentity.userName);
            user.UserId = principal.userIdentity.UID;
            user.UserHash = principal.userIdentity.userHash;

            response = _ratingService.GetRating(id);

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

        // POST: api/Rating
        [HttpPost]
        [Route("api/Rating/Create")]
        public IActionResult CreateRating(long EntityID, string title, string description)
        {
            // Implement logic to create a new rating
            IResponse response;
            IAppPrincipal principal = _securityManager.JwtToPrincipal();
            IAccountUserModel user = new AccountUserModel(principal.userIdentity.userName);
            user.UserId = principal.userIdentity.UID;
            user.UserHash = principal.userIdentity.userHash;


            response = _ratingService.CreateRating(user.UserId, EntityID, title, description);

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

        // DELETE: api/Rating/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteRating(int id)
        {
            IResponse response;
            IAppPrincipal principal = _securityManager.JwtToPrincipal();
            IAccountUserModel user = new AccountUserModel(principal.userIdentity.userName);
            user.UserId = principal.userIdentity.UID;
            user.UserHash = principal.userIdentity.userHash;

            response = _ratingService.DeleteRating(id);

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
                return Ok("The specific rating was deleted");
            }
        }

        [HttpPost]
        [Route("api/Rating/Vote")]
        public IActionResult VoteOnRating(IVote vote)
        {
            // Implement logic to create a new rating
            IResponse response;
            IAppPrincipal principal = _securityManager.JwtToPrincipal();
            IAccountUserModel user = new AccountUserModel(principal.userIdentity.userName);
            vote.VoterUID = principal.userIdentity.UID;
            user.UserHash = principal.userIdentity.userHash;


            response = _ratingService.VoteOnRating(vote);

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
    }
}
