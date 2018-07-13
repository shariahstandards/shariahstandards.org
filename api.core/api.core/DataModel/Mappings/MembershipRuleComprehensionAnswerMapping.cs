using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class MembershipRuleComprehensionAnswerMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<MembershipRuleComprehensionAnswer> E => _modelBuilder.Entity<MembershipRuleComprehensionAnswer>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.Property(x => x.Answer).IsRequired(false).HasMaxLength(1000);
            E.HasOne(x => x.MembershipRuleComprehensionQuestion).WithMany(x => x.MembershipRuleComprehensionAnswers).HasForeignKey(x => x.MembershipRuleComprehensionQuestionId);
        }
    }
}