using Team3.ThePollProject.LoggingLibrary;
using Team3.ThePollProject.Models.Response;

namespace Team3.ThePollProject.Models;

public interface ILogTarget
{
    public IResponse WriteLog(ILog log);
}