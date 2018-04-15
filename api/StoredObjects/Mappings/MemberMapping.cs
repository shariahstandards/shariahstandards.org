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
            E.Property(x => x.Introduction).IsOptional().HasMaxLength(4000);
            E.HasRequired(x => x.Organisation).WithMany(x => x.Members).HasForeignKey(x => x.OrganisationId);
        }
    }
}