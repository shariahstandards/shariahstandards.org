using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    //public class MembershipRuleExplanationMapping : IMapping
    //{
    //    private DbModelBuilder _modelBuilder;

    //    private EntityTypeConfiguration<MembershipRuleExplanation> E => _modelBuilder.Entity<MembershipRuleExplanation>();

    //    public void SetMapping(DbModelBuilder modelBuilder)
    //    {
    //        _modelBuilder = modelBuilder;
    //        E.HasKey(x => x.MembershipRuleId);
    //        E.Property(x => x.ExplanationUrl).IsOptional().HasMaxLength(400);
    //        E.HasRequired(x => x.MembershipRule).WithOptional(x => x.Explanation);
    //    }
    //}
}