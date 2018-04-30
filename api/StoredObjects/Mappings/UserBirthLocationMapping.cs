using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
  public class UserBirthLocationMapping : IMapping
  {
    private DbModelBuilder _modelBuilder;

    private EntityTypeConfiguration<UserBirthLocation> E => _modelBuilder.Entity<UserBirthLocation>();

    public void SetMapping(DbModelBuilder modelBuilder)
    {
      _modelBuilder = modelBuilder;
      E.HasKey(x => x.UserId);
      E.Property(x => x.Town).IsRequired().HasMaxLength(50);
      E.Property(x => x.Country).IsRequired().HasMaxLength(100);
      E.HasRequired(x => x.Auth0User).WithOptional(x => x.BirthLocation);
    }
  }
}
