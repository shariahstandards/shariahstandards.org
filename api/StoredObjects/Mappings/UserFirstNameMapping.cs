using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class UserFirstNameMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<UserFirstName> E => _modelBuilder.Entity<UserFirstName>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.UserId);
            E.Property(x => x.Value).IsRequired().HasMaxLength(50);
            E.HasRequired(x => x.Auth0User).WithOptional(x=>x.FirstName);
        }
    }
}
