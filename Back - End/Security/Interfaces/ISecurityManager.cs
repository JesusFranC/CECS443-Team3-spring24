using Team3.ThePollProject.Models.Response;
using Team3.ThePollProject.SecurityLibrary.Interfaces;
using Team3.ThePollProject.SecurityLibrary.Model;

namespace Team3ThePollProject.Security
{
    public interface ISecurityManager
    {
        IResponse StartLogin(string username);
        IResponse TryAuthenticating(AuthNRequest loginRequest);
        IResponse CreateIdToken(IAppPrincipal userPrincpal, DateTimeOffset timeAuthorized);
        IResponse CreateAccessToken(IAppPrincipal userPrincpal);
        IResponse CreateRefreshToken(IAppPrincipal userPrincpal);
        IResponse RefreshTokens();
        // Removing Logout, as this would simply be deleting the token from front end. No backend work required
        //IResponse Logout();
        bool isAuthorize(Dictionary<string, string> requiredClaims);
        IAppPrincipal JwtToPrincipal();
    }
}
