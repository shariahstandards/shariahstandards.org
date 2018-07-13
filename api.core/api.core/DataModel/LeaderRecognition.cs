using System;

namespace DataModel
{
    public class LeaderRecognition
    {
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }
        public int RecognisedLeaderMemberId { get; set; }
        public virtual Member RecognisedLeaderMember { get; set; }
        public DateTime LastUpdateDateTimeUtc { get; set; }
      //  public int FollowerCount { get; set; }
    }
}