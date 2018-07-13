using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class UserMobilePhoneMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<UserMobilePhone> E => _modelBuilder.Entity<UserMobilePhone>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.UserId);
            E.Property(x => x.Value).IsRequired().HasMaxLength(150);
            E.HasOne(x => x.Auth0User).WithOne(x=>x.MobilePhone).HasForeignKey<UserMobilePhone>(x=>x.UserId);
        }
    }
}
