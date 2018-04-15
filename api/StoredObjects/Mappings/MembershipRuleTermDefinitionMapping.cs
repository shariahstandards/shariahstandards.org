using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class MembershipRuleTermDefinitionMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<MembershipRuleTermDefinition> E => _modelBuilder.Entity<MembershipRuleTermDefinition>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.Property(x => x.Term).IsRequired().HasMaxLength(100);
            E.Property(x => x.Definition).IsRequired().HasMaxLength(5000);
            E.HasRequired(x => x.Organisation).WithMany(x => x.Terms).HasForeignKey(x => x.OrganisationId);
        }
    }
}