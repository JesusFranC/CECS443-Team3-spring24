namespace Team3.ThePollProject.Models
{
    public class RatingModel : IRatingModel
    {
        public RatingModel()
        {
            RatingID = 1;
            UserAccount_UID = 123;
            Title = "";
            Description = "";
            TimeOpen = DateTime.Now;
        }

        public long RatingID { get; set; }
        public long UserAccount_UID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime TimeOpen { get; set; }
    }
}
