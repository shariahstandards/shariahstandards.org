using System;

namespace DataModel
{
    public class SuggestionVoteSummary
    {
        public int SuggestionId { get; set; }
        public virtual Suggestion Suggestion { get; set; }
        public DateTime LastUpdateDateTimeUtc { get; set; }
        public int Supporters { get; set; }
        public int Opposers { get; set; }
    }
}