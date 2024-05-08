namespace Team3.ThePollProject.Models
{
    public interface IVote
    {
        bool UpOrDown { get; set; }
        long VoterUID { get; set; }
        long? VoteID { get; set; }
        long? PollID { get; set; }
        long? RatingID { get; set; }
    }
}
