using System;

namespace DataModel
{
    public class MembershipRuleComprehensionTestResult
    {
        public int Id { get; set; }
        public virtual MembershipRuleComprehensionQuestion MembershipRuleComprehensionQuestion { get; set; }
        public int MembershipRuleComprehensionQuestionId { get; set; }
        public virtual Auth0User Auth0User { get; set; }
        public string Auuth0UserId { get; set; }
        public DateTime StartedDateTimeUtc { get; set; }
        public DateTime AnsweredDateTimeUtc { get; set; }
        public bool CorrectlyAnswered { get; set; }
    }
}