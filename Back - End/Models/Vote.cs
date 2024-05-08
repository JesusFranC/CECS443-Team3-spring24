namespace Team3.ThePollProject.Models
{
    public class Vote : IVote
    {

        public bool UpOrDown { get; set; }
        public long VoterUID { get; set; }
        public long? VoteID { get; set; }
        public long? PollID { get; set; }
        public long? RatingID { get; set; }

        public Vote(bool UpOrDown, long VoterUID, long? VoteID, long? PollID, long? RatingID)
        {
            this.UpOrDown = UpOrDown;
            this.VoterUID = VoterUID;
            this.VoteID = VoteID;
            this.PollID = PollID;
            this.RatingID = RatingID;
        }
        public Vote()
        {
            UpOrDown = false;
            VoterUID = 0;
            VoteID = 0;
            PollID = 0;
            RatingID = 0;
        }

    }
}
