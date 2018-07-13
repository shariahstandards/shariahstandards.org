using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class MembershipRuleSectionMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<MembershipRuleSection> E => _modelBuilder.Entity<MembershipRuleSection>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.Property(x => x.Title).IsRequired().HasMaxLength(250);
            E.Property(x => x.UniqueInOrganisationName).IsRequired().HasMaxLength(100);
            E.HasOne(x => x.ShurahBasedOrganisation).WithMany(x => x.MembershipRuleSections).HasForeignKey(x => x.ShurahBasedOrganisationId);
        }
    }
}