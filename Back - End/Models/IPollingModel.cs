namespace Team3.ThePollProject.Models
{
    public interface IPollingModel
    {
        long? PollID { get; set; }
        long UserAccount_UID { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        DateTime TimeOpen { get; set; }
        string Option1 { get; set; }
    }
}
