using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class MembershipRuleMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<MembershipRule> E => _modelBuilder.Entity<MembershipRule>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.Property(x => x.RuleStatement).IsRequired().HasMaxLength(500);
            E.HasRequired(x => x.MembershipRuleSection).WithMany(x => x.MembershipRules).HasForeignKey(x => x.MembershipRuleSectionId);
        }
    }
}