namespace Team3.ThePollProject.Models
{
    public class PollingModel : IPollingModel
    {
        public long PollID { get; set; }
        public long UserAccount_UID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime TimeOpen { get; set; }
        public string Options { get; set; }
    }
}
