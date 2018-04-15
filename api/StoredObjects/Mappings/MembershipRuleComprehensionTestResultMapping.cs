using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class MembershipRuleComprehensionTestResultMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<MembershipRuleComprehensionTestResult> E => _modelBuilder.Entity<MembershipRuleComprehensionTestResult>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.HasRequired(x => x.Auth0User).WithMany(x => x.MembershipRuleComprehensionTestResults).HasForeignKey(x => x.Auuth0UserId);
            E.HasRequired(x => x.MembershipRuleComprehensionQuestion).WithMany(x => x.MembershipRuleComprehensionTestResults).HasForeignKey(x => x.MembershipRuleComprehensionQuestionId);
        }
    }
}