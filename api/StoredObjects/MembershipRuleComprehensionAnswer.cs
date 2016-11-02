using System;

namespace StoredObjects
{
    public class MembershipRuleComprehensionAnswer
    {
        public virtual MembershipRuleComprehensionQuestion MembershipRuleComprehensionQuestion { get; set; }
        public int MembershipRuleComprehensionQuestionId { get; set; }
        public string Answer { get; set; }
        public DateTime LastUpdatedDateTimeUtc { get; set; }
        public bool Correct { get; set; }
    }
}