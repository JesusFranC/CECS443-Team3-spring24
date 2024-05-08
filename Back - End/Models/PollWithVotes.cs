namespace Team3.ThePollProject.Models
{
    public class PollWithVotes
    {
        public IPollingModel? poll { get; set; }
        public List<IVote>? votes { get; set; }

        public PollWithVotes(IPollingModel? poll, List<IVote>? votes)
        {
            this.poll = poll;
            this.votes = votes;
        }
        public PollWithVotes() { }
    }
}
