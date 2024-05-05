using Team3.ThePollProject.SecurityLibrary.Interfaces;

namespace Team3.ThePollProject.SecurityLibrary.Model
{
    public class AuthUserModel : IAuthUserModel
    {
        public long UID { get; set; }
        public string? userName { get; set; }
        public byte[] salt { get; set; }
        public string? userHash { get; set; }
        public AuthUserModel()
        {
            UID = 0;
            userName = null;
            salt = new byte[32];
            userHash = null;
        }
        public AuthUserModel(long pUID, string pUserName, byte[] pSalt, string pUserHash)
        {
            UID = pUID;
            userName = pUserName;
            salt = pSalt;
            userHash = pUserHash;
        }
    }
}
