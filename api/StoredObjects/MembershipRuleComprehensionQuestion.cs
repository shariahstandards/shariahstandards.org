using System;

namespace StoredObjects
{
    public class MembershipRuleComprehensionQuestion
    {   public int Id { get; set; }
        public virtual MembershipRule MembershipRule { get; set; }
        public int MembershipRuleId { get; set; }
        public string Question { get; set; }
        public DateTime LastUpdatedDateTimeUtc { get; set; }
        public int RequiredCorrectAnswerMaximumTime { get; set; }
    }
}