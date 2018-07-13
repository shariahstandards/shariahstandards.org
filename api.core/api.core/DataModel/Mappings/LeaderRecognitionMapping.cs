using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
  public class LeaderRecognitionMapping : IMapping
  {
    private ModelBuilder _modelBuilder;

    private EntityTypeBuilder<LeaderRecognition> E => _modelBuilder.Entity<LeaderRecognition>();

    public void SetMapping(ModelBuilder modelBuilder)
    {
      _modelBuilder = modelBuilder;
      E.HasKey(x => x.MemberId);
      E.HasOne(x => x.Member).WithOne(x => x.LeaderRecognition).HasForeignKey<LeaderRecognition>(x => x.MemberId)
        .OnDelete(DeleteBehavior.Restrict);
      E.HasOne(x => x.RecognisedLeaderMember).WithMany(x => x.Followers).HasForeignKey(x => x.RecognisedLeaderMemberId)
        .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
