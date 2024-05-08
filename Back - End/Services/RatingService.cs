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
    public class RatingService : IRatingService
    {
        private readonly ILogService _logService;
        private readonly IGenericDAO _dao;

        public RatingService(ILogService logService, IGenericDAO dao)
        {
            _logService = logService;
            _dao = dao;
        }
        //Get all Ratings

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

        public IResponse GetRatings()
        {

            // Call a method from data access layer to retrieve all ratings
            // Log any relevant information using _logService
            // Return an IResponse object with the fetched data or any error message

            IResponse response = new Response();
            List<IRatingModel> listOfRatings = new List<IRatingModel>();
            List<object> ratingWithVotes = new List<object>();

            List<KeyValuePair<string, HashSet<SqlParameter>?>> sqlCommandList = new List<KeyValuePair<string, HashSet<SqlParameter>?>>();

            // Create SQL command text
            string commandText = "SELECT * FROM Ratings";

            // Create Key Value pairs with sql and parameters
            KeyValuePair<string, HashSet<SqlParameter>?> sqlStatement = new KeyValuePair<string, HashSet<SqlParameter>?>(commandText, null);
            sqlCommandList.Add(sqlStatement);

            try
            {
                // Attempt SQL Execution
                List<object[]> rowsReturned = _dao.ExecuteReadOnly(sqlCommandList);

                // Create return value object
                List<object> returnValue = new List<object>();
                DateTime timeNow = DateTime.Now;

                // Loop through rows
                foreach (object[] row in rowsReturned)
                {

                    // Map values from row into return object
                    IRatingModel returnedRating = new RatingModel();

                    MapValues(returnedRating, row);

                    // Add to return value object
                    returnValue.Add(returnedRating);
                }
                if (returnValue.Count == 0)
                {
                    throw new Exception("No Rows Returned");
                }
            }
            catch (Exception ex)
            {
                // Return Error if we cannot execute sql successfullly
                return createErrorResponse(ex);
            }

            foreach (var rating in listOfRatings)
            {
                commandText = "SELECT * FROM Votes WHERE RatingID = @RatingID";

                // Create return value object
                List<IVote> tempVoteList = new List<IVote>();

                // Create Key Value pairs with sql and parameters
                HashSet<SqlParameter> parameters = [CreateParameter("@RatingID", SqlDbType.BigInt, rating.RatingID)];

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

                RatingWithVotes tempRatingWithVotes = new RatingWithVotes(rating, tempVoteList);
                ratingWithVotes.Add(tempRatingWithVotes);
            }

            IResponse successResponse = new Response();
            successResponse.ReturnValue = ratingWithVotes;
            return successResponse;
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
            IRatingModel rating = new RatingModel();

            #region Get All the Ratings
            // Create SQL command list
            List<KeyValuePair<string, HashSet<SqlParameter>?>> sqlCommandList = new List<KeyValuePair<string, HashSet<SqlParameter>?>>();

            // Create SQL command text
            string commandText = "SELECT * FROM Ratings WHERE RatingID = @RatingID";

            // Create Key Value pairs with sql and parameters
            HashSet<SqlParameter> parameters = [CreateParameter("@RatingID", SqlDbType.BigInt, id)];

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
                    IRatingModel returnedRating = new RatingModel();
                    MapValues(returnedRating, row);

                    // Add to return value object
                    rating = returnedRating;
                }
                if (rating == new RatingModel())
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
            commandText = "SELECT * FROM Votes WHERE RatingID = @RatingID";

            // Create return value object
            List<IVote> tempVoteList = new List<IVote>();

            // Create Key Value pairs with sql and parameters
            parameters = [CreateParameter("@RatingID", SqlDbType.BigInt, rating.RatingID)];


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

            RatingWithVotes tempRatingWithVotes = new RatingWithVotes(rating, tempVoteList);
            #endregion

            IResponse successResponse = new Response();
            successResponse.ReturnValue = [tempRatingWithVotes];
            return successResponse;
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

        public IResponse VoteOnRating(IVote vote)
        {
            IResponse response = new Response();

            try
            {
                if (vote.RatingID == null)
                {
                    response.HasError = true;
                    response.ErrorMessage = "Rating ID is null";
                    return response;
                }

                var sqlCommand = (@"
                    INSERT INTO Vote (UpOrDown, VoterUID, VoteID, RatingID)
                    VALUES (@UpOrDown, @VoterUID, @VoteID, @RatingID);
                ");

                HashSet<SqlParameter> parameters = new HashSet<SqlParameter>
                {
                    new SqlParameter("@UpOrDown", vote.UpOrDown),
                    new SqlParameter("@VoterUID", vote.VoterUID),
                    new SqlParameter("@VoteID", vote.VoteID),
                    new SqlParameter("@RatingID", vote.RatingID),
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
                _logService.CreateLogAsync("Error", "Data", $"Failed to create vote: {ex.Message}", null);

                response.HasError = true;
                response.ErrorMessage = "Failed to create vote";
                return response;
            }
            response.HasError = false;
            return response;
        }
    }
}
