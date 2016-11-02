using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class MemberMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<Member> E => _modelBuilder.Entity<Member>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.Property(x => x.Email).IsRequired().HasMaxLength(250);
            E.Property(x => x.Introduction).IsOptional().HasMaxLength(4000);
            E.Property(x => x.Name).IsRequired().HasMaxLength(200);
            E.Property(x => x.Phone).IsOptional().HasMaxLength(20);
            E.HasRequired(x => x.Organisation).WithMany(x => x.Members).HasForeignKey(x => x.OrganisationId);
        }
    }
}