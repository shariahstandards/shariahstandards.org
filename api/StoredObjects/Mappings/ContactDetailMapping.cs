using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class ContactDetailMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<ContactDetail> E => _modelBuilder.Entity<ContactDetail>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.Property(x => x.Value).IsRequired().HasMaxLength(250);
            E.HasRequired(x => x.Auth0User).WithMany(x => x.ContactDetails).HasForeignKey(x => x.Auth0UserId);
        }
    }
}