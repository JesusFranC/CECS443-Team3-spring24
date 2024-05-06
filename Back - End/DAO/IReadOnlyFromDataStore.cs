using Microsoft.Data.SqlClient;
using Team3.ThePollProject.Models.Response;

namespace Team3.ThePollProject.DAO;
public interface IReadOnlyFromDataStore
{
    public List<object[]> ExecuteReadOnly(ICollection<KeyValuePair<string, HashSet<SqlParameter>?>> sqlCommands);

    public IResponse ExecuteReadOnly(SqlCommand sqlCommand);
}