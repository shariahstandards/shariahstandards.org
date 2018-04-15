using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class MembershipRuleSectionRelationshipMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<MembershipRuleSectionRelationship> E => _modelBuilder.Entity<MembershipRuleSectionRelationship>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.MembershipRuleSectionId);
            E.HasRequired(x => x.MembershipRuleSection).WithOptional(x => x.ParentMembershipRuleSection);
            E.HasRequired(x => x.ParentMembershipRuleSection).WithMany(x => x.ChildMembershipRuleSections).HasForeignKey(x => x.ParentMembershipRuleSectionId);

        }
    }
}