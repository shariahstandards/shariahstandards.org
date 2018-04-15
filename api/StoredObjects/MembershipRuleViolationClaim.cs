using System;

namespace StoredObjects
{
    public class MembershipRuleViolationAccusation
    {
        public int Id { get; set; }
        public int MembershipRuleId { get; set; }
        public virtual MembershipRule MembershipRule { get; set; }
        public string ExplanationOfClaim { get; set; }
        public int ClaimingMemberId { get; set; }
        public virtual Member ClaimingMember { get; set; }
        public int AccusedMemberId { get; set; }
        public virtual Member AccusedMember { get; set; }
        public string RequestedRemedy { get; set; }
        public DateTime RecordeDateTimeUtc { get; set; }
        public virtual MembershipRuleViolationJudgement Judgement { get; set; }
    }
}