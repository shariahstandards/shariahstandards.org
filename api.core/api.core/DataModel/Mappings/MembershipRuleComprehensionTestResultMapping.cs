using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class MembershipRuleComprehensionTestResultMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<MembershipRuleComprehensionTestResult> E => _modelBuilder.Entity<MembershipRuleComprehensionTestResult>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.HasOne(x => x.Auth0User).WithMany(x => x.MembershipRuleComprehensionTestResults).HasForeignKey(x => x.Auuth0UserId);
            E.HasOne(x => x.MembershipRuleComprehensionQuestion).WithMany(x => x.MembershipRuleComprehensionTestResults).HasForeignKey(x => x.MembershipRuleComprehensionQuestionId);
        }
    }
}