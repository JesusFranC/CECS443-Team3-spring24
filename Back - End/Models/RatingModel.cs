namespace Team3.ThePollProject.Models
{
    public class RatingModel : IRatingModel
    {
        public long RatingID { get; set; }
        public long UserAccount_UID { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime TimeOpen { get; set; }
    }
}
