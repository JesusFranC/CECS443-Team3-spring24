
using Team3.ThePollProject.Models;
using Team3.ThePollProject.Models.Response;

namespace Team3.ThePollProject.LoggingLibrary
{
    public interface ILogService
    {
        IResponse CreateLog(string logLevel, string category, string message, string? userHash);
        Task<IResponse> CreateLogAsync(string logLevel, string category, string message, string? userHash);

    }
}