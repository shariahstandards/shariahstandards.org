namespace StoredObjects
{
    public class MembershipRuleTermDefinition
    {
        public virtual ShurahBasedOrganisation Organisation { get; set; }
        public int OrganisationId { get; set; }
        public int Id { get; set; }
        public string Term { get; set; }
        public string Definition { get; set; }
    }
}