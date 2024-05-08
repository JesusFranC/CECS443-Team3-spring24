using Microsoft.Data.SqlClient;
using Team3.ThePollProject.DataAccess;
using Team3.ThePollProject.LoggingLibrary;
using Team3.ThePollProject.Model;
using Team3.ThePollProject.Models.Response;

namespace Team3.ThePollProject.Services
{
    public class RatingService
    {
        private readonly ILogService _logService;
        private readonly IGenericDAO _dao;

        public RatingService(ILogService logService, IGenericDAO dao)
        {
            _logService = logService;
            _dao = dao;
        }
        //Get all Ratings
        public IResponse GetRatings()
        {

            // Call a method from data access layer to retrieve all ratings
            // Log any relevant information using _logService
            // Return an IResponse object with the fetched data or any error message

            IResponse response = new Response();

            try
            {
                // Generate SqlCommand to select all ratings along with corresponding votes
                var sqlCommand = new SqlCommand(@"
                    SELECT r.*, v.VoteID, v.UpOrDown, v.VoterUID
                    FROM Ratings r
                    LEFT JOIN Vote v ON r.RatingID = v.RatingID
                ");

                // Call data access layer method to execute the command
                response = _dao.ExecuteReadOnly(sqlCommand);

                // Log success message
                _logService.CreateLogAsync("Info", "Data", "Retrieved all polls along with corresponding votes successfully.", null);



                response.HasError = false;
                return response;
            }
            catch (Exception ex)
            {
                // Log error message
                _logService.CreateLogAsync("Info", "Data", "Failed to retrieve all ratings along with corresponding votes successfully.", null);
                response.HasError = true;
                response.ErrorMessage = "Failed to retrieve all ratings along with corresponding votes successfully.";
                return response;
            }
        }
        // Get a rating by ID

        public IResponse GetEntities()
        {

            // Call a method from data access layer to retrieve all entities
            // Log any relevant information using _logService
            // Return an IResponse object with the fetched data or any error message

            IResponse response = new Response();

            try
            {
                // Generate SqlCommand to select all ratings along with corresponding votes
                var sqlCommand = new SqlCommand(@"
                    SELECT *
                    FROM RatingEntities
                ");

                // Call data access layer method to execute the command
                response = _dao.ExecuteReadOnly(sqlCommand);

                // Log success message
                _logService.CreateLogAsync("Info", "Data", "Retrieved all Entities .", null);



                response.HasError = false;
                return response;
            }
            catch (Exception ex)
            {
                // Log error message
                _logService.CreateLogAsync("Info", "Data", "Failed to retrieve all Entities.", null);
                response.HasError = true;
                response.ErrorMessage = "Failed to retrieve all Entities ";
                return response;
            }
        }

        public IResponse GetRating(long id)
        {
            // Call a method from data access layer to retrieve the rating by ID
            // Log any relevant information using _logService
            // Return an IResponse object with the fetched rating or any error message

            IResponse response = new Response();

            try
            {
                // Generate SqlCommand to select a rating by ID along with corresponding votes
                var sqlCommand = new SqlCommand(@"
                    SELECT r.*, v.VoteID, v.UpOrDown, v.VoterUID
                    FROM Ratings r
                    LEFT JOIN Vote v ON r.RatingID = v.RatingID
                    WHERE r.RatingID = @RatingID
                ");
                sqlCommand.Parameters.AddWithValue("@RatingID", id);

                // Call data access layer method to execute the command
                response = _dao.ExecuteReadOnly(sqlCommand);

                // Log success message
                _logService.CreateLogAsync("Info", "Data", "Retrieved rating with ID {id} along with corresponding votes successfully.", null);

                response.HasError = false;
                return response;
            }
            catch (Exception ex)
            {
                // Log error message
                _logService.CreateLogAsync("Info", "Data", "Failed to retrieve rating with ID {id} along with corresponding votes: {ex.Message}", null);
                response.HasError = true;
                response.ErrorMessage = $"Failed to retrieve rating with ID {id} along with corresponding votes.";
                return response;
            }
        }

        // Create a new rating
        public IResponse CreateRating(long UserUID, long EntityID, string title, string description)
        {
            // Call a method from data access layer to create a new rating
            // Log any relevant information using _logService
            // Return an IResponse object indicating the success/failure of the create operation
            IResponse response = new Response();

            try
            {


                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description))
                {
                    response.HasError = true;
                    response.ErrorMessage = "Title or description are null";
                    return response;
                }

                var sqlCommand = (@"
                    INSERT INTO Ratings (UserUID, Title, Description, TimeOpen)
                    VALUES (@UserUID, @Title, @Description, @TimeOpen);
                ");

                DateTime currentTime = DateTime.Now;

                HashSet<SqlParameter> parameters = new HashSet<SqlParameter>
                {
                    new SqlParameter("@UserUID", UserUID),
                    new SqlParameter("@EntityID", EntityID),
                    new SqlParameter("@Title", title),
                    new SqlParameter("@Description", description),
                    new SqlParameter("@TimeOpen", currentTime),
                };

                var sqlCommands = new List<KeyValuePair<string, HashSet<SqlParameter>?>>();
                sqlCommands.Add(new KeyValuePair<string, HashSet<SqlParameter>?>(sqlCommand, parameters));

                var daoValue = _dao.ExecuteWriteOnly(sqlCommands);
                response.ReturnValue = new List<object>()
            {
                daoValue
            };
            }
            catch (Exception ex)
            {
                _logService.CreateLogAsync("Error", "Data", $"Failed to create rating: {ex.Message}", null);

                response.HasError = true;
                response.ErrorMessage = "Failed to create rating";
                return response;
            }
            response.HasError = false;
            return response;
        }

        public IResponse DeleteRating(long id)
        {
            // Call a method from data access layer to delete the rating by ID
            // Log any relevant information using _logService
            // Return an IResponse object with the fetched rating or any error message

            IResponse response = new Response();

            try
            {
                // Generate SqlCommand to select a rating by ID and corresponding votes should delete on cascade
                var sqlCommand = @"
                    DELETE FROM Ratings WHERE RatingID = @RatingID
                ";

                var parameters = new HashSet<SqlParameter>
                {
                    new SqlParameter("@RatingID",id)
                };


                var sqlCommands = new List<KeyValuePair<string, HashSet<SqlParameter>?>>();
                sqlCommands.Add(new KeyValuePair<string, HashSet<SqlParameter>?>(sqlCommand, parameters));

                // Call data access layer method to execute the command
                var daoValue = _dao.ExecuteWriteOnly(sqlCommands);
                response.ReturnValue = new List<object>()
                {
                    daoValue
                };

                // Log success message
                _logService.CreateLogAsync("Info", "Data", "Deleted rating with ID {id} along with corresponding votes successfully.", null);

                response.HasError = false;
                return response;
            }
            catch (Exception ex)
            {
                // Log error message
                _logService.CreateLogAsync("Info", "Data", "Failed to delete rating with ID {id} along with corresponding votes: {ex.Message}", null);
                response.HasError = true;
                response.ErrorMessage = $"Failed to delete rating with ID {id} along with corresponding votes.";
                return response;
            }
        }
    }
}
