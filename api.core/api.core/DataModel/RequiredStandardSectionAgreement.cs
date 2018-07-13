namespace DataModel
{
    public class RequiredStandardSectionAgreement
    {
        public int Id { get; set; }
        public virtual ShurahBasedOrganisation ShurahBasedOrganisation { get; set; }
        public int ShurahBasedOrganisationId { get; set; }
        public virtual StandardSection StandardSection { get; set; }
    }
}