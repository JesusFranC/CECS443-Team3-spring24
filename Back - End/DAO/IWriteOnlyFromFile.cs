using Team3.ThePollProject.Models.Response;
namespace Team3.ThePollProject.DAO;
public interface IWriteOnlyFromFile
{
    public IResponse ExecuteWriteOnly(string value);
}
