using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class MembershipRuleComprehensionQuestionMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<MembershipRuleComprehensionQuestion> E => _modelBuilder.Entity<MembershipRuleComprehensionQuestion>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.Property(x => x.Question).IsOptional().HasMaxLength(1000);
            E.HasRequired(x => x.MembershipRule).WithMany(x => x.MembershipRuleComprehensionQuestions).HasForeignKey(x => x.MembershipRuleId);
        }
    }
}