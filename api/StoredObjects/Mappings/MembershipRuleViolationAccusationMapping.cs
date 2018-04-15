using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class MembershipRuleViolationAccusationMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<MembershipRuleViolationAccusation> E => _modelBuilder.Entity<MembershipRuleViolationAccusation>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.Property(x => x.ExplanationOfClaim).IsRequired().HasMaxLength(4000);
            E.Property(x => x.RequestedRemedy).IsRequired().HasMaxLength(4000);
            E.HasRequired(x => x.MembershipRule).WithMany(x => x.MembershipRuleViolationClaims).HasForeignKey(x => x.MembershipRuleId)
                .WillCascadeOnDelete(false);
            E.HasRequired(x => x.AccusedMember).WithMany(x => x.ReceivedMembershipRuleViolationAccusations).HasForeignKey(x => x.AccusedMemberId);
            E.HasRequired(x => x.ClaimingMember).WithMany(x => x.MadeMembershipRuleViolationAccusations).HasForeignKey(x => x.ClaimingMemberId)
                .WillCascadeOnDelete(false);

        }
    }
}