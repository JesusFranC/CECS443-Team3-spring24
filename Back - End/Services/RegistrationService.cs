using Team3.ThePollProject.Models.Response;
using Team3.ThePollProject.DataAccess;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading;
using Team3.ThePollProject.Model;
using System.Diagnostics.Contracts;

namespace Team3.ThePollProject.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IGenericDAO _dao;

        public RegistrationService(IGenericDAO genericDAO)
        {
            _dao = genericDAO;
        }

        public IResponse MakeUser(string email)
        {
            IResponse response = new Response();

            if (DoesEmailExist(email))
            {
                response.HasError = true;
                response.ErrorMessage = "User already exists.";
                return response;
            }

            /// Generate random values for Salt and UserHash
            Random rand = new Random();
            int salt = rand.Next();
            string userHash = Guid.NewGuid().ToString(); // Example random hash, you can use your own hash generation logic here

            // Construct SQL commands to create user, OTP, and user claim
            string createUserSql = "INSERT INTO UserAccount (UserName, Salt, UserHash) VALUES (@UserName, @Salt, @UserHash);";
            var createUserParams = new HashSet<SqlParameter>
            {
                new SqlParameter("@UserName", email),
                new SqlParameter("@Salt", salt),
                new SqlParameter("@UserHash", userHash)
            };

            _dao.ExecuteWriteOnly(new List<KeyValuePair<string, HashSet<SqlParameter>?>> {
                new KeyValuePair<string, HashSet<SqlParameter>?>(createUserSql, createUserParams) }) ;

            string createOTPSql = "INSERT INTO OTP (UID, PassHash, attempts, firstFailedLogin) " +
                                  "SELECT UID, @PassHash, @Attempts, @FirstFailedLogin " +
                                  "FROM UserAccount WHERE UserName = @UserName";

            var createOTPParams = new HashSet<SqlParameter>
            {
                new SqlParameter("@UserName", email), // Use email to filter the UserAccount table
                new SqlParameter("@PassHash", "tempPassHash"), // Example temporary pass hash
                new SqlParameter("@Attempts", 1), // Example initial attempts
                new SqlParameter("@FirstFailedLogin", DateTime.Now) // Example initial failed login time
            };

            string createUserClaimSql = "INSERT INTO UserClaim (UID, ClaimID, ClaimScope) " +
                             "SELECT UID, 0, 'true' " +
                             "FROM UserAccount WHERE UserName = @UserName";

            var createUserClaimParams = new HashSet<SqlParameter>
            {
                new SqlParameter("@UserName", email) // Use email to filter the UserAccount table
            };

            // Execute SQL commands
            _dao.ExecuteWriteOnly(new List<KeyValuePair<string, HashSet<SqlParameter>?>> {
                new KeyValuePair<string, HashSet<SqlParameter>?>(createOTPSql, createOTPParams),
                new KeyValuePair<string, HashSet<SqlParameter>?>(createUserClaimSql, createUserClaimParams)
            });

            response.HasError = false;
            return response;
        }

        public bool DoesEmailExist(string email)
        {
            IResponse response = new Response();

            // Construct SQL command to check if email exists in DB
            string sqlCommand = "SELECT COUNT(*) FROM UserAccount WHERE UserName = @UserName";
            var parameters = new HashSet<SqlParameter>
            {
                new SqlParameter("@UserName", email)
            };

            SqlCommand command = new SqlCommand(sqlCommand);
            foreach (var parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }


            response = _dao.ExecuteReadOnly(command);

            if (response.HasError)
            {
                return true;
            }
            else
            {
                // Check if the ReturnValue is not null and contains at least one item
                if (response.ReturnValue != null && response.ReturnValue.Count > 0)
                {
                    // Get the first item from the collection
                    var firstItemArray = response.ReturnValue.First() as object[];

                    if (firstItemArray != null && firstItemArray.Length > 0)
                    {
                        var firstItem = firstItemArray[0];

                        // Attempt to convert the first item to an integer
                        if (firstItem is int intValue)
                        {
                            // Successfully converted to an integer
                            return intValue > 0;
                        }
                        else
                        {
                            // Try parsing the string representation of the first item as an integer
                            if (int.TryParse(firstItem.ToString(), out int parsedValue))
                            {
                                return parsedValue > 0;
                            }
                        }
                    }
                }
            }

            return false;
        }

    }
}
