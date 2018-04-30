using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class UserEmailMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<UserEmail> E => _modelBuilder.Entity<UserEmail>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.UserId);
            E.Property(x => x.Value).IsRequired().HasMaxLength(150);
            E.HasRequired(x => x.Auth0User).WithOptional(x=>x.Email);
        }
    }
}
