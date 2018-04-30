using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class WordPartPositionTypeMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<WordPartPositionType> E => _modelBuilder.Entity<WordPartPositionType>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Code);
            E.Property(x => x.Code).IsRequired().HasMaxLength(10);
    }
    }
}
