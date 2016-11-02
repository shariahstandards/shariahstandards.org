using System;

namespace StoredObjects
{
    public class ArchivedMembershipRule
    {
        public int MembershipRuleId { get; set; }
        public virtual MembershipRule MembershipRule { get; set; }
        public int Id { get; set; }
        public string RuleStatement { get; set; }
        public DateTime PublishedDateTimeUtc { get; set; }
    }
}