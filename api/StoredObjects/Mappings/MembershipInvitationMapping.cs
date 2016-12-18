using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class MembershipInvitationMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<MembershipInvitation> E => _modelBuilder.Entity<MembershipInvitation>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.Property(x => x.EmailAddressList).IsOptional().HasMaxLength(4000);
            E.HasRequired(x => x.InviterMember).WithMany(x => x.Invitations).HasForeignKey(x => x.InviterMemberId);
        }
    }
}