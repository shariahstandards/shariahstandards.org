using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
  public class UserBirthLocationMapping : IMapping
  {
    private ModelBuilder _modelBuilder;

    private EntityTypeBuilder<UserBirthLocation> E => _modelBuilder.Entity<UserBirthLocation>();

    public void SetMapping(ModelBuilder modelBuilder)
    {
      _modelBuilder = modelBuilder;
      E.HasKey(x => x.UserId);
      E.Property(x => x.Town).IsRequired().HasMaxLength(50);
      E.Property(x => x.Country).IsRequired().HasMaxLength(100);
      E.HasOne(x => x.Auth0User).WithOne(x => x.BirthLocation).HasForeignKey<UserBirthLocation>(x=>x.UserId);
    }
  }
}
