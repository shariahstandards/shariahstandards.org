using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class ContactDetailMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<ContactDetail> E => _modelBuilder.Entity<ContactDetail>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.Property(x => x.Value).IsRequired().HasMaxLength(250);
            E.HasOne(x => x.Auth0User).WithMany(x => x.ContactDetails).HasForeignKey(x => x.Auth0UserId);
        }
    }
}
