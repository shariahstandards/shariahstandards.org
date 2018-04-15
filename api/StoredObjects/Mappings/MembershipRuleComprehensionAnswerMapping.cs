using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class MembershipRuleComprehensionAnswerMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<MembershipRuleComprehensionAnswer> E => _modelBuilder.Entity<MembershipRuleComprehensionAnswer>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.Property(x => x.Answer).IsOptional().HasMaxLength(1000);
            E.HasRequired(x => x.MembershipRuleComprehensionQuestion).WithMany(x => x.MembershipRuleComprehensionAnswers).HasForeignKey(x => x.MembershipRuleComprehensionQuestionId);
        }
    }
}