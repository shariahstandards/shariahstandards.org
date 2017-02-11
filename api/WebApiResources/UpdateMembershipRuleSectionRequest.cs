namespace WebApiResources
{
    public class UpdateMembershipRuleSectionRequest
    {
        public int MembershipRuleSectionId { get; set; }
        public string Title { get; set; }
        public string UniqueUrlSlug { get; set; }
    }
}