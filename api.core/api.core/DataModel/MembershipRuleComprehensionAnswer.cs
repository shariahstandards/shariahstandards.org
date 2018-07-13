using System;

namespace DataModel
{
    public class MembershipRuleComprehensionAnswer
    {
        public int Id { get; set; }
        public virtual MembershipRuleComprehensionQuestion MembershipRuleComprehensionQuestion { get; set; }
        public int MembershipRuleComprehensionQuestionId { get; set; }
        public string Answer { get; set; }
        public DateTime LastUpdatedDateTimeUtc { get; set; }
        public bool Correct { get; set; }
    }
}