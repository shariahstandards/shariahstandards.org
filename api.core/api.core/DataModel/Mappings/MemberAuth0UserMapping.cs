using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class MemberAuth0UserMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<MemberAuth0User> E => _modelBuilder.Entity<MemberAuth0User>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.HasOne(x => x.Member).WithMany(x => x.MemberAuth0Users).HasForeignKey(x => x.MemberId);
            E.HasOne(x => x.Auth0User).WithMany(x => x.MemberAuth0Users).HasForeignKey(x => x.Auth0UserId);
        }
    }
}