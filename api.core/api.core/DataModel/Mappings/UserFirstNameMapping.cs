using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
  public class UserFirstNameMapping : IMapping
  {
    private ModelBuilder _modelBuilder;

    private EntityTypeBuilder<UserFirstName> E => _modelBuilder.Entity<UserFirstName>();

    public void SetMapping(ModelBuilder modelBuilder)
    {
      _modelBuilder = modelBuilder;
      E.HasKey(x => x.UserId);
      E.Property(x => x.Value).IsRequired().HasMaxLength(50);
      E.HasOne(x => x.Auth0User).WithOne(x => x.FirstName).HasForeignKey<UserFirstName>(x => x.UserId);
    }
  }
}
