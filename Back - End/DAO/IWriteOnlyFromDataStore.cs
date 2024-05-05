namespace Team3.ThePollProject.DAO;
public interface IWriteOnlyFromDataStore
{
    public int ExecuteWriteOnly(ICollection<KeyValuePair<string, HashSet<SqlParameter>?>> sqlCommands);
}
