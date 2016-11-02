using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class MembershipApplicationAcceptanceMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<MembershipApplicationAcceptance> E => _modelBuilder.Entity<MembershipApplicationAcceptance>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.HasRequired(x => x.AcceptingMember)
                .WithMany(x => x.MemberAcceptances)
                .HasForeignKey(x => x.AcceptingMemberId);
            E.HasRequired(x => x.MembershipApplication)
                .WithMany(x => x.Acceptances)
                .HasForeignKey(x => x.MembershipApplicationId).WillCascadeOnDelete(false);
        }
    }
}