namespace Team3.ThePollProject.Models;

public interface ILogTarget
{
    public IResponse WriteLog(ILog log);
}