namespace Team3.ThePollProject.Models
{
    public class RatingWithVotes
    {
        public IRatingModel? rating { get; set; }
        public List<IVote>? votes { get; set; }

        public RatingWithVotes(IRatingModel? poll, List<IVote>? votes)
        {
            this.rating = rating;
            this.votes = votes;
        }
        public RatingWithVotes() { }
    }
}
