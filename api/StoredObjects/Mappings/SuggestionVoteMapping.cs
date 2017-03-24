using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class SuggestionVoteMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<SuggestionVote> E => _modelBuilder.Entity<SuggestionVote>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.HasRequired(x => x.Suggestion).WithMany(x => x.Votes).HasForeignKey(x => x.SuggestionId);
            E.HasRequired(x => x.VoterMember).WithMany(x => x.SuggestionVotes).HasForeignKey(x => x.VoterMemberId).WillCascadeOnDelete(false);
            E.HasOptional(x => x.VotingLeaderMember).WithMany(x => x.SuggestionFollowerVotes).HasForeignKey(x => x.VotingLeaderMemberId).WillCascadeOnDelete(false);

        }
    }
}