using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class WordPartFormMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<WordPartForm> E => _modelBuilder.Entity<WordPartForm>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Text);
            E.Property(x => x.Text).IsRequired().HasMaxLength(100);
        }
    }
}
