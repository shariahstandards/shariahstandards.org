using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class MembershipRuleSectionMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<MembershipRuleSection> E => _modelBuilder.Entity<MembershipRuleSection>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.Property(x => x.Title).IsRequired().HasMaxLength(250);
            E.Property(x => x.UniqueInOrganisationName).IsRequired().HasMaxLength(100);
            E.HasRequired(x => x.ShurahBasedOrganisation).WithMany(x => x.MembershipRuleSections).HasForeignKey(x => x.ShurahBasedOrganisationId);
        }
    }
}