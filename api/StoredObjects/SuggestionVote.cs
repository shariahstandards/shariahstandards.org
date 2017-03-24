using System;
using System.Data.Entity.Spatial;

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
        public int? VotingLeaderMemberId { get; set; }
        public virtual Member VotingLeaderMember { get; set; }
    }
    public class SuggestionComment
    {
        public int Id { get; set; }
        public int SuggestionId { get; set; }
        public virtual Suggestion Suggestion { get; set; }
        public int CommentingMemberId { get; set; }
        public virtual Member CommentingMember { get; set; }
        public bool? CommentIsSupportingSuggestion { get; set; }
        public DateTime LastUpdateDateTimeUtc { get; set; }
        public string Comment { get; set; }
        public bool IsCensored { get; set; }
    }
}