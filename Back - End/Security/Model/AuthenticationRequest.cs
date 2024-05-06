using Team3.ThePollProject.Model;

namespace Team3.ThePollProject.SecurityLibrary
{
    public class AuthenticationRequest
    {
        public AuthenticationRequest(IAccountUserModel userIdentity, string proof)
        {
            UserIdentity = userIdentity ?? throw new ArgumentNullException(nameof(userIdentity));
            Proof = proof ?? throw new ArgumentNullException();
        }
        public IAccountUserModel UserIdentity { get; set; }
        // use Proof because it is a generic term
        public string Proof { get; set; } = string.Empty;
    }
}
