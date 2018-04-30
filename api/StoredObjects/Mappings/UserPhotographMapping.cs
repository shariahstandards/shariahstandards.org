using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class UserPhotographMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<UserPhotograph> E => _modelBuilder.Entity<UserPhotograph>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.UserId);
            E.Property(x => x.Value).IsRequired().HasMaxLength(500*1000);
            E.HasRequired(x => x.Auth0User).WithOptional(x=>x.Photograph);
        }
    }
}
