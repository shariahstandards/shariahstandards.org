using System;
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
        public virtual IList<SuggestionVote> Votes { get; set; }
        public virtual IList<SuggestionComment> Comments { get; set; }
        public bool Removed { get; set; }
        public bool PendingModeration { get; set; }
        public DateTime CreatedDateUtc { get; set; }
    }
}