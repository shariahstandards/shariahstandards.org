using System;
using System.Collections.Generic;

namespace StoredObjects
{
    public class MembershipRule
    {
        public int MembershipRuleSectionId { get; set; }
        public virtual MembershipRuleSection MembershipRuleSection { get; set; }
        public int Id { get; set; }
        public string RuleStatement { get; set; }
        public DateTime PublishedDateTimeUtc { get; set; }
        public virtual IList<ArchivedMembershipRule> ArchivedMembershipRules { get; set; }
        public bool Active { get; set; }
        public int NumberOfCorrectAnswersRequired { get; set; }
        public virtual IList<MembershipRuleViolationAccusation> MembershipRuleViolationClaims { get; set; }
    }
}