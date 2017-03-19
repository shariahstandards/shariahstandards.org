using System;

namespace StoredObjects
{
    public class SuggestionVote
    {
        public int Id { get; set; }
        public int SuggestionId { get; set; }
        public virtual Suggestion Suggestion { get; set; }
        public int VoterMemberId { get; set; }
        public virtual Member VoterMember { get; set; }
        public bool? MemberIsSupportingSuggestion { get; set; }
        public DateTime LastUpdateDateTimeUtc { get; set; }
        public int DelegatedVoteCount { get; set; }
        public bool VoteByLeader { get; set; }
    }
}