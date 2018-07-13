using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
  public class OrganisationLeaderMapping : IMapping
  {
    private ModelBuilder _modelBuilder;

    private EntityTypeBuilder<OrganisationLeader> E => _modelBuilder.Entity<OrganisationLeader>();

    public void SetMapping(ModelBuilder modelBuilder)
    {
      _modelBuilder = modelBuilder;
      E.HasKey(x => x.OrganisationId);
      E.HasOne(x => x.Organisation).WithOne(x => x.OrganisationLeader)
        .HasForeignKey<OrganisationLeader>(x => x.OrganisationId);
      E.HasOne(x => x.Leader).WithMany().HasForeignKey(x => x.LeaderMemberId);
    }
  }
}
