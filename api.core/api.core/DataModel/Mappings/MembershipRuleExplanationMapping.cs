using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    //public class MembershipRuleExplanationMapping : IMapping
    //{
    //    private ModelBuilder _modelBuilder;

    //    private EntityTypeBuilder<MembershipRuleExplanation> E => _modelBuilder.Entity<MembershipRuleExplanation>();

    //    public void SetMapping(ModelBuilder modelBuilder)
    //    {
    //        _modelBuilder = modelBuilder;
    //        E.HasKey(x => x.MembershipRuleId);
    //        E.Property(x => x.ExplanationUrl).IsRequired(false).HasMaxLength(400);
    //        E.HasOne(x => x.MembershipRule).WithOptional(x => x.Explanation);
    //    }
    //}
}