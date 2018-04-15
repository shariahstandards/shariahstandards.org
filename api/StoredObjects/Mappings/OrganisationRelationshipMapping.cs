using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class OrganisationRelationshipMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<OrganisationRelationship> E => _modelBuilder.Entity<OrganisationRelationship>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.ShurahBasedOrganisationId);
            E.HasRequired(x => x.ShurahBasedOrganisation).WithOptional(x => x.ParentOrganisationRelationship);
            E.HasRequired(x => x.ParentOrganisation).WithMany(x => x.ChildOrganisationRelationships).HasForeignKey(x=>x.ParentOrganisationId);
        }
    }
}