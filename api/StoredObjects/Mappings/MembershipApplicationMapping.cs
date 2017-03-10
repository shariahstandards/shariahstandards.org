using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class MembershipApplicationMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<MembershipApplication> E => _modelBuilder.Entity<MembershipApplication>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.Property(x => x.Email).IsRequired().HasMaxLength(250);
            E.Property(x => x.Auth0UserId).IsRequired().HasMaxLength(200);
            E.Property(x => x.SupportingStatement).IsOptional().HasMaxLength(2000);
            E.Property(x => x.PhoneNumber).IsOptional().HasMaxLength(20);
            E.HasRequired(x => x.Organisation).WithMany(x=>x.MembershipApplications).HasForeignKey(x=>x.OrganisationId);
            E.HasRequired(x => x.Auth0User).WithMany(x => x.MembershipApplications).HasForeignKey(x => x.Auth0UserId);
        }
    }
}