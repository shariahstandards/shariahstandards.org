using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class MembershipRuleViolationJudgementMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<MembershipRuleViolationJudgement> E => _modelBuilder.Entity<MembershipRuleViolationJudgement>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.MembershipRuleViolationAccusationId);
            E.HasRequired(x => x.MembershipRuleViolationAccusation).WithOptional(x => x.Judgement);
            E.Property(x => x.Remedy).IsRequired().HasMaxLength(4000);
            E.Property(x => x.RulingExplanation).IsOptional().HasMaxLength(null);
        }
    }
}