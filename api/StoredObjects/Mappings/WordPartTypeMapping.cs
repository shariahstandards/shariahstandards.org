using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class WordPartTypeMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<WordPartType> E => _modelBuilder.Entity<WordPartType>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Code);
            E.Property(x => x.Code).IsRequired().HasMaxLength(10);
    }
    }
}
