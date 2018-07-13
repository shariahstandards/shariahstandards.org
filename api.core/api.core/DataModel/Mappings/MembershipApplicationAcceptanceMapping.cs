using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class MembershipApplicationAcceptanceMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<MembershipApplicationAcceptance> E => _modelBuilder.Entity<MembershipApplicationAcceptance>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.HasOne(x => x.AcceptingMember)
                .WithMany(x => x.MemberAcceptances)
                .HasForeignKey(x => x.AcceptingMemberId);
            E.HasOne(x => x.MembershipApplication)
                .WithMany(x => x.Acceptances)
                .HasForeignKey(x => x.MembershipApplicationId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}