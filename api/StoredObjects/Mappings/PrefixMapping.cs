using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class PrefixMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<Prefix> E => _modelBuilder.Entity<Prefix>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Text);
            E.Property(x => x.Text).IsRequired().HasMaxLength(20);
    }
    }
}
