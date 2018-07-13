using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class RootMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<Root> E => _modelBuilder.Entity<Root>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Text);
            E.Property(x => x.Text).IsRequired().HasMaxLength(20);
    }
    }
}
