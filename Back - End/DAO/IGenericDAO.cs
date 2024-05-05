using Team3.ThePollProject.DAO;

namespace Team3.ThePollProject.DataAccess;

public interface IGenericDAO : IReadOnlyFromDataStore, IWriteOnlyFromDataStore, IReadOnlyFromFile, IWriteOnlyFromFile
{
}
