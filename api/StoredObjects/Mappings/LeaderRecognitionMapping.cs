using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class LeaderRecognitionMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<LeaderRecognition> E => _modelBuilder.Entity<LeaderRecognition>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.MemberId);
            E.HasRequired(x => x.Member).WithOptional(x => x.LeaderRecognition);
            E.HasRequired(x => x.RecognisedLeaderMember).WithMany(x => x.Followers).HasForeignKey(x=>x.RecognisedLeaderMemberId);
        }
    }
}