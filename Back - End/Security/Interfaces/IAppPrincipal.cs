namespace Team3.ThePollProject.SecurityLibrary.Interfaces
{
    public interface IAppPrincipal
    {
        public IAuthUserModel userIdentity { get; set; }
        public ICollection<KeyValuePair<string, string>> claims { get; set; }
    }
}
