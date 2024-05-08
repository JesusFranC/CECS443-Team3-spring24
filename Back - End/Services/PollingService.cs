using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;
using Team3.ThePollProject.DataAccess;
using Team3.ThePollProject.LoggingLibrary;
using Team3.ThePollProject.Model;
using Team3.ThePollProject.Models;
using Team3.ThePollProject.Models.Response;

namespace Team3.ThePollProject.Services
{
    public class PollingService : IPollingService
    {
        private readonly ILogService _logService;
        private readonly IGenericDAO _dao;

        public PollingService(ILogService logService, IGenericDAO dao)
        {
            _logService = logService;
            _dao = dao;
        }

        #region Utility Functions
        private static void MapValues(object obj, object[] values)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            if (values == null)
                throw new ArgumentNullException(nameof(values));

            var properties = obj.GetType().GetProperties();

            for (int i = 0; i < Math.Min(properties.Length, values.Length); i++)
            {
                PropertyInfo property = properties[i];
                object value = values[i];

                // Check if property is writable and value is compatible
                if (property.CanWrite && (value == null || property.PropertyType.IsAssignableFrom(value.GetType())))
                {
                    property.SetValue(obj, value);
                }
                else
                {
                    throw new ArgumentException($"Value at index {i} is not assignable to property '{property.Name}'.");
                }
            }
        }
        private IResponse createSuccessResponse(object? item)
        {
            // Pack it up and ship it
            IResponse successResponse = new Response();
            successResponse.HasError = false;
            if (item is not null)
            {
                successResponse.ReturnValue = new List<object>() { item };
            }
            return successResponse;
        }
        private IResponse createErrorResponse(Exception ex)
        {
            IResponse errorResponse = new Response();
            errorResponse.HasError = true;
            errorResponse.ErrorMessage = ex.Message;
            return errorResponse;
        }
        private static SqlParameter CreateParameter(string paramName, SqlDbType sqlType, object? value)
        {
            SqlParameter parameter = new SqlParameter(paramName, sqlType);
            if (value is null)
            {
                value = DBNull.Value;
            }
            parameter.Value = value;
            return parameter;
        }
        private static HashSet<SqlParameter> RemoveSqlParameter(HashSet<SqlParameter> parameters, string paramName)
        {
            var parameterToRemove = parameters.FirstOrDefault(p => p.ParameterName == paramName);
            if (parameterToRemove != null)
            {
                parameters.Remove(parameterToRemove);
            }
            return parameters;
        }
        private static bool IsPrimitiveType(Type type)
        {
            return type.IsPrimitive || type == typeof(string) || type == typeof(DateTime);
        }
        private static SqlDbType GetSqlType(Type type)
        {
            if (type == typeof(string) || type == typeof(string))
            {
                return SqlDbType.VarChar;
            }
            else if (type == typeof(long) || type == typeof(long?))
            {
                return SqlDbType.BigInt;
            }
            else if (type == typeof(int) || type == typeof(int?))
            {
                return SqlDbType.Int;
            }
            else if (type == typeof(float) || type == typeof(float?))
            {
                return SqlDbType.Float;
            }
            else if (type == typeof(DateTime))
            {
                return SqlDbType.DateTime;
            }
            else
            {
                throw new ArgumentException("Unsupported data type for SQL parameter.");
            }
        }
        private static HashSet<SqlParameter> CreateSqlParameters(object obj)
        {
            HashSet<SqlParameter> parameters = new HashSet<SqlParameter>();

            if (obj == null)
                return parameters;

            Type objectType = obj.GetType();

            if (IsPrimitiveType(objectType))
            {
                // Create a single parameter for primitive types
                parameters.Add(CreateParameter("@value", GetSqlType(objectType), obj));
            }
            else
            {
                var properties = objectType.GetProperties();

                foreach (var property in properties)
                {
                    try
                    {
                        string paramName = "@" + property.Name;

                        SqlDbType sqlType = GetSqlType(property.PropertyType);

                        SqlParameter parameter = new SqlParameter(paramName, sqlType);
                        if (property.GetValue(obj) is not null)
                        {
                            parameter.Value = property.GetValue(obj);
                        }
                        else
                        {
                            parameter.Value = DBNull.Value;
                        }
                        parameters.Add(parameter);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            return parameters;
        }
        #endregion


        // Get all polls
        public IResponse GetPolls()
        {
            List<IPollingModel> listOfPolls = new List<IPollingModel>();
            List<object> pollWithVotes = new List<object>();

            #region Get All the Polls
            // Create SQL command list
            List<KeyValuePair<string, HashSet<SqlParameter>?>> sqlCommandList = new List<KeyValuePair<string, HashSet<SqlParameter>?>>();

            // Create SQL command text
            string commandText = "SELECT * FROM Polls";

            // Create Key Value pairs with sql and parameters
            KeyValuePair<string, HashSet<SqlParameter>?> sqlStatement = new KeyValuePair<string, HashSet<SqlParameter>?>(commandText, null);
            sqlCommandList.Add(sqlStatement);

            try
            {
                // Attempt SQL Execution
                List<object[]> rowsReturned = _dao.ExecuteReadOnly(sqlCommandList);

                // Loop through rows
                foreach (object[] row in rowsReturned)
                {

                    // Map values from row into return object
                    IPollingModel returnedPoll = new PollingModel(null, 0, "", "", DateTime.Now, "Option1", "Option2");
                    MapValues(returnedPoll, row);

                    // Add to return value object
                    listOfPolls.Add(returnedPoll);
                }
                if (listOfPolls.Count == 0)
                {
                    throw new Exception("No Rows Returned");
                }
            }
            catch (Exception ex)
            {
                // Return Error if we cannot execute sql successfullly
                return createErrorResponse(ex);
            }
            #endregion

            #region Get Votes for each poll
            foreach (var poll in listOfPolls)
            {
                commandText = "SELECT * FROM Votes WHERE PollID = @PollID";

                // Create return value object
                List<IVote> tempVoteList = new List<IVote>();

                // Create Key Value pairs with sql and parameters
                HashSet<SqlParameter> parameters = [CreateParameter("@PollID", SqlDbType.BigInt, poll.PollID)];

                sqlStatement = new KeyValuePair<string, HashSet<SqlParameter>?>(commandText, parameters);
                sqlCommandList.Add(sqlStatement);

                try
                {
                    // Attempt SQL Execution
                    List<object[]> rowsReturned = _dao.ExecuteReadOnly(sqlCommandList);

                    // Loop through rows
                    foreach (object[] row in rowsReturned)
                    {
                        // Map values from row into return object
                        IVote tempVote = new Vote();
                        MapValues(tempVote, row);

                        // Add to return value object
                        tempVoteList.Add(tempVote);
                    }
                }
                catch (Exception ex)
                {
                    // Return Error if we cannot execute sql successfullly
                    return createErrorResponse(ex);
                }

                PollWithVotes tempPollWithVotes = new PollWithVotes(poll, tempVoteList);
                pollWithVotes.Add(tempPollWithVotes);
            }
            #endregion
            IResponse successResponse = new Response();
            successResponse.ReturnValue = pollWithVotes;
            return successResponse;
        }

        // Get a poll by ID
        public IResponse GetPoll(long id)
        {
            IPollingModel poll = new PollingModel();

            #region Get All the Polls
            // Create SQL command list
            List<KeyValuePair<string, HashSet<SqlParameter>?>> sqlCommandList = new List<KeyValuePair<string, HashSet<SqlParameter>?>>();

            // Create SQL command text
            string commandText = "SELECT * FROM Polls WHERE PollID = @PollID";

            // Create Key Value pairs with sql and parameters
            HashSet<SqlParameter> parameters = [CreateParameter("@PollID", SqlDbType.BigInt, id)];

            KeyValuePair<string, HashSet<SqlParameter>?> sqlStatement = new KeyValuePair<string, HashSet<SqlParameter>?>(commandText, parameters);
            sqlCommandList.Add(sqlStatement);

            try
            {
                // Attempt SQL Execution
                List<object[]> rowsReturned = _dao.ExecuteReadOnly(sqlCommandList);

                // Loop through rows
                foreach (object[] row in rowsReturned)
                {

                    // Map values from row into return object
                    IPollingModel returnedPoll = new PollingModel();
                    MapValues(returnedPoll, row);

                    // Add to return value object
                    poll = returnedPoll;
                }
                if (poll == new PollingModel())
                {
                    throw new Exception("No Rows Returned");
                }
            }
            catch (Exception ex)
            {
                // Return Error if we cannot execute sql successfullly
                return createErrorResponse(ex);
            }
            #endregion

            #region Get Votes for each 
            commandText = "SELECT * FROM Votes WHERE PollID = @PollID";

            // Create return value object
            List<IVote> tempVoteList = new List<IVote>();

            // Create Key Value pairs with sql and parameters
            parameters = [CreateParameter("@PollID", SqlDbType.BigInt, poll.PollID)];


            sqlStatement = new KeyValuePair<string, HashSet<SqlParameter>?>(commandText, parameters);
            sqlCommandList.Add(sqlStatement);

            try
            {
                // Attempt SQL Execution
                List<object[]> rowsReturned = _dao.ExecuteReadOnly(sqlCommandList);

                // Loop through rows
                foreach (object[] row in rowsReturned)
                {
                    // Map values from row into return object
                    IVote tempVote = new Vote();
                    MapValues(tempVote, row);

                    // Add to return value object
                    tempVoteList.Add(tempVote);
                }
            }
            catch (Exception ex)
            {
                // Return Error if we cannot execute sql successfullly
                return createErrorResponse(ex);
            }

            PollWithVotes tempPollWithVotes = new PollWithVotes(poll, tempVoteList);
            #endregion

            IResponse successResponse = new Response();
            successResponse.ReturnValue = [tempPollWithVotes];
            return successResponse;
        }


        // Create a new poll
        public IResponse CreatePoll(long UserUID, string title, string description, string pollOption1, string pollOption2)
        {
            // Create sql command list object
            List<KeyValuePair<string, HashSet<SqlParameter>?>> sqlCommandList = new List<KeyValuePair<string, HashSet<SqlParameter>?>>();

            // Create sql command text
            string commandText = "INSERT INTO Polls (UserAccount_UID, Title, Description, TimeOpen, Option1, Option2) " +
                "VALUES (@UserAccount_UID, @Title, @Description, @TimeOpen, @Option1, @Option2)";

            // Create Parameters
            IPollingModel poll = new PollingModel(null, UserUID, title, description, DateTime.UtcNow, pollOption1, pollOption2);
            HashSet<SqlParameter> parameters = CreateSqlParameters(poll);

            // Teak Parameters
            parameters = RemoveSqlParameter(parameters, "@PollID");

            //Create Key value pair with sql and parameters
            KeyValuePair<string, HashSet<SqlParameter>?> sqlStatement = new KeyValuePair<string, HashSet<SqlParameter>?>(commandText, parameters);
            sqlCommandList.Add(sqlStatement);
            try
            {
                // Attempt SQL Execution
                int rowsAffected = _dao.ExecuteWriteOnly(sqlCommandList);
                if (rowsAffected == 0) { throw new Exception("No rows affected"); }
                return createSuccessResponse(null);
            }
            catch (Exception ex)
            {
                return createErrorResponse(ex);
            }
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

