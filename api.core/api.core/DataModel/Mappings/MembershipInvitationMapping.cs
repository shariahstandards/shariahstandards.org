using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class MembershipInvitationMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<MembershipInvitation> E => _modelBuilder.Entity<MembershipInvitation>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.Property(x => x.EmailAddressList).IsRequired(false).HasMaxLength(4000);
            E.HasOne(x => x.InviterMember).WithMany(x => x.Invitations).HasForeignKey(x => x.InviterMemberId);
        }
    }
}