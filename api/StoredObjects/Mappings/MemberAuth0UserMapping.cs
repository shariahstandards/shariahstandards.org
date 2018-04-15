using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class MemberAuth0UserMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<MemberAuth0User> E => _modelBuilder.Entity<MemberAuth0User>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.HasRequired(x => x.Member).WithMany(x => x.MemberAuth0Users).HasForeignKey(x => x.MemberId);
            E.HasRequired(x => x.Auth0User).WithMany(x => x.MemberAuth0Users).HasForeignKey(x => x.Auth0UserId);
        }
    }
}