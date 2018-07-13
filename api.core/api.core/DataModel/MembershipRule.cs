using System;
using System.Collections.Generic;

namespace DataModel
{
    public class MembershipRule
    {
        public int MembershipRuleSectionId { get; set; }
        public virtual MembershipRuleSection MembershipRuleSection { get; set; }
        public int Id { get; set; }
        public int Sequence { get; set; }
        public string RuleStatement { get; set; }
        public DateTime PublishedDateTimeUtc { get; set; }
        public virtual IList<ArchivedMembershipRule> ArchivedMembershipRules { get; set; }
        public int NumberOfCorrectAnswersRequired { get; set; }
        public virtual IList<MembershipRuleViolationAccusation> MembershipRuleViolationClaims { get; set; }
        public virtual IList<MembershipRuleComprehensionQuestion> MembershipRuleComprehensionQuestions { get; set; }
        //public virtual MembershipRuleExplanation Explanation { get; set; }
    }
}