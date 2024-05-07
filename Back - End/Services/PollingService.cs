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
            IResponse response = new Response();

            try
            {
                if (poll == null)
                {
                    response.HasError = true;
                    response.ErrorMessage = "Rating model is null";
                    return response;
                }

                if (string.IsNullOrEmpty(poll.Title) || string.IsNullOrEmpty(poll.Description))
                {
                    response.HasError = true;
                    response.ErrorMessage = "Title and description are null";
                    return response;
                }

                var sqlCommand = (@"
                    INSERT INTO Polls (UserUID, Title, Description, TimeOpen, Options)
                    VALUES (@UserUID, @Title, @Description, @TimeOpen, @Options);
                ");

                HashSet<SqlParameter> parameters = new HashSet<SqlParameter>
                {
                    new SqlParameter("@UserUID", poll.UserAccount_UID),
                    new SqlParameter("@Title", poll.Title),
                    new SqlParameter("@Description", poll.Description),
                    new SqlParameter("@TimeOpen", poll.TimeOpen),
                    new SqlParameter("@Options", poll.Options)
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
                _logService.CreateLogAsync("Error", "Data", $"Failed to create poll: {ex.Message}", null);

                response.HasError = true;
                response.ErrorMessage = "Failed to create poll";
                return response;
            }
            response.HasError = false;
            return response;
        }

        // Update an existing poll


        // Delete a poll by ID
        public IResponse DeletePoll(long id)
        {
            // Call a method from data access layer to delete the poll by ID
            // Log any relevant information using _logService
            // Return an IResponse object indicating the success/failure of the delete operation
            IResponse response = new Response();

            try
            {
                // Generate SqlCommand to select a rating by ID and corresponding votes should delete on cascade
                var sqlCommand = @"
                    DELETE FROM Polls WHERE PollID = @PollID
                ";

                var parameters = new HashSet<SqlParameter>
                {
                    new SqlParameter("@PollID",id)
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
                _logService.CreateLogAsync("Info", "Data", "Deleted poll with ID {id} along with corresponding votes successfully.", null);

                response.HasError = false;
                return response;
            }
            catch (Exception ex)
            {
                // Log error message
                _logService.CreateLogAsync("Info", "Data", "Failed to delete poll with ID {id} along with corresponding votes: {ex.Message}", null);
                response.HasError = true;
                response.ErrorMessage = $"Failed to delete poll with ID {id} along with corresponding votes.";
                return response;

            }
        }
    }
}

