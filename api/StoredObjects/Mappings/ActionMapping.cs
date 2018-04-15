using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class ActionMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<Action> E => _modelBuilder.Entity<Action>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.Property(x => x.Description).IsRequired().HasMaxLength(500);
            E.HasRequired(x => x.Organisation).WithMany(x => x.Actions).HasForeignKey(x => x.OrganisationId).WillCascadeOnDelete(false);
        }
    }
}