using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class WordPartPositionTypeMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<WordPartPositionType> E => _modelBuilder.Entity<WordPartPositionType>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Code);
            E.Property(x => x.Code).IsRequired().HasMaxLength(10);
    }
    }
}
