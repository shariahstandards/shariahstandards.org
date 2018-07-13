namespace DataModel
{
    public class MembershipRuleSectionRelationship
    {
        public int MembershipRuleSectionId { get; set; }
        public virtual MembershipRuleSection MembershipRuleSection { get; set; }
        public int ParentMembershipRuleSectionId { get; set; }
        public virtual MembershipRuleSection ParentMembershipRuleSection { get; set; }
    }
}
