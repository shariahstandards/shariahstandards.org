using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class MembershipRuleSectionAcceptanceMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<MembershipRuleSectionAcceptance> E => _modelBuilder.Entity<MembershipRuleSectionAcceptance>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.HasRequired(x => x.Auth0User).WithMany(x => x.MembershipRuleSectionAcceptances).HasForeignKey(x => x.Auth0UserId);
            E.HasRequired(x => x.MembershipRuleSection).WithMany(x => x.MembershipRuleSectionAcceptances).HasForeignKey(x => x.MembershipRuleSectionId);
        }
    }
}