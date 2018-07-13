using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class PrefixMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<Prefix> E => _modelBuilder.Entity<Prefix>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Text);
            E.Property(x => x.Text).IsRequired().HasMaxLength(20);
    }
    }
}
