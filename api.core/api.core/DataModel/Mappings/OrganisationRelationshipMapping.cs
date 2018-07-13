using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
  public class OrganisationRelationshipMapping : IMapping
  {
    private ModelBuilder _modelBuilder;

    private EntityTypeBuilder<OrganisationRelationship> E => _modelBuilder.Entity<OrganisationRelationship>();

    public void SetMapping(ModelBuilder modelBuilder)
    {
      _modelBuilder = modelBuilder;
      E.HasKey(x => x.ShurahBasedOrganisationId);
      E.HasOne(x => x.ShurahBasedOrganisation).WithOne(x => x.ParentOrganisationRelationship)
        .HasForeignKey<OrganisationRelationship>(x => x.ShurahBasedOrganisationId);
      E.HasOne(x => x.ParentOrganisation).WithMany(x => x.ChildOrganisationRelationships).HasForeignKey(x => x.ParentOrganisationId);
    }
  }
}
