namespace Team3.ThePollProject.Models
{
    public class PollingModel : IPollingModel
    {
        public long? PollID { get; set; }
        public long UserAccount_UID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime TimeOpen { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public PollingModel(long? PollID, long UserAccount_UID, string Title, string Description, DateTime TimeOpen, string Option1, string Option2)
        {
            this.PollID = PollID;
            this.UserAccount_UID = UserAccount_UID;
            this.Title = Title;
            this.Description = Description;
            this.TimeOpen = TimeOpen;
            this.Option1 = Option1;
            this.Option2 = Option2;
        }
        public PollingModel()
        {
            this.PollID = 0;
            this.UserAccount_UID = 0;
            this.Title = "Title";
            this.Description = "Description";
            this.TimeOpen = DateTime.Now;
            this.Option1 = "Option1";
            this.Option2 = "Option1";
        }
    }
}
