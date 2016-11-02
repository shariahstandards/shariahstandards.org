namespace StoredObjects
{
    public class MembershipRuleSectionRelationship
    {
        public int MembershipRuleSectionId { get; set; }
        public MembershipRuleSection MembershipRuleSection { get; set; }
        public int ParentMembershipRuleSectionId { get; set; }
        public MembershipRuleSection ParentMembershipRuleSection { get; set; }
    }
}