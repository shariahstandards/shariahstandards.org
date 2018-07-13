using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class MembershipRuleTermDefinitionMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<MembershipRuleTermDefinition> E => _modelBuilder.Entity<MembershipRuleTermDefinition>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.Property(x => x.Term).IsRequired().HasMaxLength(100);
            E.Property(x => x.Definition).IsRequired().HasMaxLength(5000);
            E.HasOne(x => x.Organisation).WithMany(x => x.Terms).HasForeignKey(x => x.OrganisationId);
        }
    }
}