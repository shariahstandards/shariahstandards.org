using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class MembershipApplicationMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<MembershipApplication> E => _modelBuilder.Entity<MembershipApplication>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.Property(x => x.Email).IsRequired().HasMaxLength(250);
            E.Property(x => x.Auth0UserId).IsRequired().HasMaxLength(200);
            E.Property(x => x.SupportingStatement).IsRequired(false).HasMaxLength(2000);
            E.Property(x => x.PhoneNumber).IsRequired(false).HasMaxLength(20);
            E.HasOne(x => x.Organisation).WithMany(x=>x.MembershipApplications).HasForeignKey(x=>x.OrganisationId);
            E.HasOne(x => x.Auth0User).WithMany(x => x.MembershipApplications).HasForeignKey(x => x.Auth0UserId);
        }
    }
}