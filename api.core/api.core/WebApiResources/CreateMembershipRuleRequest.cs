namespace WebApiResources
{
    public class CreateMembershipRuleRequest
    {
        public int MembershipRuleSectionId { get; set; }
        public string Rule{ get; set; }
    }
    public class UpdateMembershipRuleRequest
    {
        public int MembershipRuleId { get; set; }
        public string Rule { get; set; }
    }
    public class DeleteMembershipRuleRequest
    {
        public int MembershipRuleId { get; set; }
    }

    public class DragAndDropMembershipRuleRequest
    {
        public int DroppedMembershipRuleId { get; set; }
        public int DraggedMembershipRuleId { get; set; }
    }
}