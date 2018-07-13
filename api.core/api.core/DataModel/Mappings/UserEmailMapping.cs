using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
  public class UserEmailMapping : IMapping
  {
    private ModelBuilder _modelBuilder;

    private EntityTypeBuilder<UserEmail> E => _modelBuilder.Entity<UserEmail>();

    public void SetMapping(ModelBuilder modelBuilder)
    {
      _modelBuilder = modelBuilder;
      E.HasKey(x => x.UserId);
      E.Property(x => x.Value).IsRequired().HasMaxLength(150);
      E.HasOne(x => x.Auth0User).WithOne(x => x.Email).HasForeignKey<UserEmail>(x => x.UserId);
    }
  }
}
