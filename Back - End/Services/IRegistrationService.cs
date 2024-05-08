using Team3.ThePollProject.Models.Response;

namespace Team3.ThePollProject.Services
{
    public interface IRegistrationService
    {
        bool DoesEmailExist(string email);
        IResponse MakeUser(string email);
    }
}