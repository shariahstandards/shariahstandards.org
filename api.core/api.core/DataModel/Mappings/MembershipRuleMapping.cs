using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class MembershipRuleMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<MembershipRule> E => _modelBuilder.Entity<MembershipRule>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.Property(x => x.RuleStatement).IsRequired().HasMaxLength(500);
            E.HasOne(x => x.MembershipRuleSection).WithMany(x => x.MembershipRules).HasForeignKey(x => x.MembershipRuleSectionId);
        }
    }
}