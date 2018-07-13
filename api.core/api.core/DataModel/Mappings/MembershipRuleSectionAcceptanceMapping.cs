using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class MembershipRuleSectionAcceptanceMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<MembershipRuleSectionAcceptance> E => _modelBuilder.Entity<MembershipRuleSectionAcceptance>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.HasOne(x => x.Auth0User).WithMany(x => x.MembershipRuleSectionAcceptances).HasForeignKey(x => x.Auth0UserId);
            E.HasOne(x => x.MembershipRuleSection).WithMany(x => x.MembershipRuleSectionAcceptances).HasForeignKey(x => x.MembershipRuleSectionId);
        }
    }
}