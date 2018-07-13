using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
  public class MembershipRuleSectionRelationshipMapping : IMapping
  {
    private ModelBuilder _modelBuilder;

    private EntityTypeBuilder<MembershipRuleSectionRelationship> E => _modelBuilder.Entity<MembershipRuleSectionRelationship>();

    public void SetMapping(ModelBuilder modelBuilder)
    {
      _modelBuilder = modelBuilder;
      E.HasKey(x => x.MembershipRuleSectionId);
      E.HasOne(x => x.MembershipRuleSection).WithOne(x => x.ParentMembershipRuleSection)
        .HasForeignKey<MembershipRuleSectionRelationship>(x => x.MembershipRuleSectionId)
        .OnDelete(DeleteBehavior.Restrict);
      E.HasOne(x => x.ParentMembershipRuleSection).WithMany(x => x.ChildMembershipRuleSections)
        .HasForeignKey(x => x.ParentMembershipRuleSectionId)
        .OnDelete(DeleteBehavior.Restrict);
    }
  }
}
