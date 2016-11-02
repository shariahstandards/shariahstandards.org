using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class OrganisationLeaderMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<OrganisationLeader> E => _modelBuilder.Entity<OrganisationLeader>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.OrganisationId);
            E.HasRequired(x => x.Organisation).WithOptional(x => x.OrganisationLeader);
            E.HasRequired(x => x.Leader).WithMany().HasForeignKey(x=>x.LeaderMemberId);
        }
    }
}