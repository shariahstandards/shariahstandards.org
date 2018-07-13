using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class SuggestionVoteMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<SuggestionVote> E => _modelBuilder.Entity<SuggestionVote>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.HasOne(x => x.Suggestion).WithMany(x => x.Votes).HasForeignKey(x => x.SuggestionId);
            E.HasOne(x => x.VoterMember).WithMany(x => x.SuggestionVotes).HasForeignKey(x => x.VoterMemberId).OnDelete(DeleteBehavior.Restrict);
            E.HasOne(x => x.VotingLeaderMember).WithMany(x => x.SuggestionFollowerVotes).HasForeignKey(x => x.VotingLeaderMemberId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
