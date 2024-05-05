namespace Team3.ThePollProject.SecurityLibrary.Model
{
    public class AuthNRequest
    {
        public string username { get; set; }
        public string otp { get; set; }
        public AuthNRequest(string username, string otp)
        {
            this.username = username;
            this.otp = otp;
        }
    }
}
