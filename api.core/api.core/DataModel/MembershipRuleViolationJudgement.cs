using System;

namespace DataModel
{
    public class MembershipRuleViolationJudgement
    {
        public int MembershipRuleViolationAccusationId { get; set; }
        public virtual MembershipRuleViolationAccusation MembershipRuleViolationAccusation { get; set; }
        public string Remedy { get; set; }
        public string RulingExplanation { get; set; }
        public bool RemedyCompleted { get; set; }
        public DateTime RecordeDateTimeUtc { get; set; }
    }
}