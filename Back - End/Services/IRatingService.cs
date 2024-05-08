using Team3.ThePollProject.Models.Response;

namespace Team3.ThePollProject.Services
{
    public interface IRatingService
    {
        IResponse GetRating(long id);
        IResponse GetRatings();
        IResponse CreateRating(long UserUID, long EntityID, string title, string description);
        IResponse GetEntities();
        IResponse DeleteRating(long id);
    }
}
