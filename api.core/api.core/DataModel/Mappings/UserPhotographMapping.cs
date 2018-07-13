using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
  public class UserPhotographMapping : IMapping
  {
    private ModelBuilder _modelBuilder;

    private EntityTypeBuilder<UserPhotograph> E => _modelBuilder.Entity<UserPhotograph>();

    public void SetMapping(ModelBuilder modelBuilder)
    {
      _modelBuilder = modelBuilder;
      E.HasKey(x => x.UserId);
      E.Property(x => x.Value).IsRequired().HasMaxLength(500 * 1000);
      E.HasOne(x => x.Auth0User).WithOne(x => x.Photograph).HasForeignKey<UserPhotograph>(x => x.UserId);
    }
  }
}
