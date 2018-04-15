namespace StoredObjects
{
    public class OrganisationRelationship
    {
        public int ShurahBasedOrganisationId { get; set; }
        public virtual ShurahBasedOrganisation ShurahBasedOrganisation { get; set; }
        public int ParentOrganisationId { get; set; }
        public virtual ShurahBasedOrganisation ParentOrganisation { get; set; }

    }
}