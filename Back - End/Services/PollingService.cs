using Microsoft.Data.SqlClient;
using Team3.ThePollProject.DataAccess;
using Team3.ThePollProject.LoggingLibrary;
using Team3.ThePollProject.Model;
using Team3.ThePollProject.Models;
using Team3.ThePollProject.Models.Response;

namespace Team3.ThePollProject.Services
{
    public class PollingService
    {
        private readonly ILogService _logService;
        private readonly IGenericDAO _dao;

        public PollingService(ILogService logService, IGenericDAO dao)
        {
            _logService = logService;
            _dao = dao;
        }

        // Get all polls
        public IResponse GetPolls()
        {
            // Call a method from data access layer to retrieve all polls
            // Log any relevant information using _logService
            // Return an IResponse object with the fetched data or any error message

            IResponse response = new Response();

            try
            {
                // Generate SqlCommand to select all polls along with corresponding votes
                var sqlCommand = new SqlCommand(@"
                    SELECT p.*, v.VoteID, v.UpOrDown, v.VoterUID
                    FROM Polls p
                    LEFT JOIN Vote v ON p.PollID = v.PollID
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
                _logService.CreateLogAsync("Info", "Data", "Failed to retrieve all polls along with corresponding votes successfully.", null);
                response.HasError = true;
                response.ErrorMessage = "Failed to retrieve all polls along with corresponding votes successfully.";
                return response;
            }
        }

        // Get a poll by ID
        public IResponse GetPoll(long id)
        {
            // Call a method from data access layer to retrieve the poll by ID
            // Log any relevant information using _logService
            // Return an IResponse object with the fetched poll or any error message

            IResponse response = new Response();

            try
            {
                // Generate SqlCommand to select a poll by ID along with corresponding votes
                var sqlCommand = new SqlCommand(@"
                    SELECT p.*, v.VoteID, v.UpOrDown, v.VoterUID
                    FROM Polls p
                    LEFT JOIN Vote v ON p.PollID = v.PollID
                    WHERE p.PollID = @PollID
                ");
                sqlCommand.Parameters.AddWithValue("@PollID", id);

                // Call data access layer method to execute the command
                response = _dao.ExecuteReadOnly(sqlCommand);

                // Log success message
                _logService.CreateLogAsync("Info", "Data", "Retrieved poll with ID {id} along with corresponding votes successfully.", null);

                response.HasError = false;
                return response;
            }
            catch (Exception ex)
            {
                // Log error message
                _logService.CreateLogAsync("Info", "Data", "Failed to retrieve poll with ID {id} along with corresponding votes: {ex.Message}", null);
                response.HasError = true;
                response.ErrorMessage = $"Failed to retrieve poll with ID {id} along with corresponding votes.";
                return response;
            }
        }

        // Create a new poll
        public IResponse CreatePoll(PollingModel poll)
        {
            // Call a method from data access layer to create a new poll
            // Log any relevant information using _logService
            // Return an IResponse object indicating the success/failure of the create operation
        }

        // Update an existing poll
        public IResponse UpdatePoll(long id, PollingModel poll)
        {
            // Call a method from data access layer to update the existing poll
            // Log any relevant information using _logService
            // Return an IResponse object indicating the success/failure of the update operation
        }

        // Delete a poll by ID
        public IResponse DeletePoll(long id)
        {
            // Call a method from data access layer to delete the poll by ID
            // Log any relevant information using _logService
            // Return an IResponse object indicating the success/failure of the delete operation
        }
    }
}

