using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class UnmodifiedWordMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<UnmodifiedWordPart> E => _modelBuilder.Entity<UnmodifiedWordPart>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Text);
            E.Property(x => x.Text).IsRequired().HasMaxLength(20);
    }
    }
}
