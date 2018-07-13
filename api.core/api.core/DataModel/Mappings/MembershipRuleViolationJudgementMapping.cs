using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class MembershipRuleViolationJudgementMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<MembershipRuleViolationJudgement> E => _modelBuilder.Entity<MembershipRuleViolationJudgement>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.MembershipRuleViolationAccusationId);
            E.HasOne(x => x.MembershipRuleViolationAccusation).WithOne(x => x.Judgement).HasForeignKey< MembershipRuleViolationJudgement>(x=>x.MembershipRuleViolationAccusationId);
            E.Property(x => x.Remedy).IsRequired().HasMaxLength(4000);
            E.Property(x => x.RulingExplanation).IsRequired(false);
        }
    }
}
