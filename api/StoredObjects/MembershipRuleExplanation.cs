namespace StoredObjects
{
    public class MembershipRuleExplanation
    {
        public virtual MembershipRule MembershipRule { get; set; }
        public int MembershipRuleId { get; set; }
        public string ExplanationUrl { get; set; }
    }
}