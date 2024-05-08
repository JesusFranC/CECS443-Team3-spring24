using Team3.ThePollProject.Models.Response;

namespace Team3.ThePollProject.Services
{
    public interface IPollingService
    {
        IResponse GetPolls();

        IResponse GetPoll(long id);

        IResponse CreatePoll(long UserUID, string title, string description, string pollOption1, string pollOption2);

        IResponse DeletePoll(long id);
    }
}
