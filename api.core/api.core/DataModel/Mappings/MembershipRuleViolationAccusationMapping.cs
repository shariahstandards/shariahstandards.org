using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class MembershipRuleViolationAccusationMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<MembershipRuleViolationAccusation> E => _modelBuilder.Entity<MembershipRuleViolationAccusation>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.Property(x => x.ExplanationOfClaim).IsRequired().HasMaxLength(4000);
            E.Property(x => x.RequestedRemedy).IsRequired().HasMaxLength(4000);
            E.HasOne(x => x.MembershipRule).WithMany(x => x.MembershipRuleViolationClaims).HasForeignKey(x => x.MembershipRuleId)
                .OnDelete(DeleteBehavior.Restrict);
            E.HasOne(x => x.AccusedMember).WithMany(x => x.ReceivedMembershipRuleViolationAccusations).HasForeignKey(x => x.AccusedMemberId);
            E.HasOne(x => x.ClaimingMember).WithMany(x => x.MadeMembershipRuleViolationAccusations).HasForeignKey(x => x.ClaimingMemberId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}