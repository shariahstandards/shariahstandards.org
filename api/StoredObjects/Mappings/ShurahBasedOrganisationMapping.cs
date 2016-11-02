using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class ShurahBasedOrganisationMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<ShurahBasedOrganisation> E => _modelBuilder.Entity<ShurahBasedOrganisation>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.Property(x => x.Name).IsRequired().HasMaxLength(250);
            E.Property(x => x.Description).IsOptional().HasMaxLength(4000);
            E.Property(x => x.UrlDomain).IsOptional().HasMaxLength(200);
        }
    }
}