using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class UserFathersFirstNameMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<UserFathersFirstName> E => _modelBuilder.Entity<UserFathersFirstName>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.UserId);
            E.Property(x => x.Value).IsRequired().HasMaxLength(50);
            E.HasOne(x => x.Auth0User).WithOne(x=>x.FathersName).HasForeignKey< UserFathersFirstName>(x=>x.UserId);
        }
    }
}
