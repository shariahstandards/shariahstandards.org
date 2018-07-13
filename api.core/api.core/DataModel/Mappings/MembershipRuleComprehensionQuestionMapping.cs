using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class MembershipRuleComprehensionQuestionMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<MembershipRuleComprehensionQuestion> E => _modelBuilder.Entity<MembershipRuleComprehensionQuestion>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.Property(x => x.Question).IsRequired(false).HasMaxLength(1000);
            E.HasOne(x => x.MembershipRule).WithMany(x => x.MembershipRuleComprehensionQuestions).HasForeignKey(x => x.MembershipRuleId);
        }
    }
}