using System.Collections.Generic;

namespace StoredObjects
{
    public class Suggestion
    {
        public int Id { get; set; }
        public string ShortDescription { get; set; }
        public string FullText { get; set; }
        public int AuthorMemberId { get; set; }
        public virtual Member AuthorMember { get; set; }
        public virtual IList<SuggestionVote> SuggestionVotes { get; set; }
        bool Removed { get; set; }
        bool VotingAllowed { get; set; }
        bool PendingModeration { get; set; }
    }
}