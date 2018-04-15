using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class Auth0UserMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<Auth0User> E => _modelBuilder.Entity<Auth0User>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x=>x.Id);
            E.Property(x => x.Id).HasMaxLength(200);
            E.Property(x => x.Name).IsRequired().HasMaxLength(200);
            E.Property(x => x.PictureUrl).IsRequired().HasMaxLength(1000);
        }
    }
}