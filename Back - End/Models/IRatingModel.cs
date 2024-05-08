namespace Team3.ThePollProject.Models
{
    public interface IRatingModel
    {
        long RatingID { get; set; }
        long UserAccount_UID { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        public DateTime TimeOpen { get; set; }
    }
}
