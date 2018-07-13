using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class WordPartTypeMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<WordPartType> E => _modelBuilder.Entity<WordPartType>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Code);
            E.Property(x => x.Code).IsRequired().HasMaxLength(10);
    }
    }
}
