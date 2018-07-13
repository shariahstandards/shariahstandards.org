using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class HumanMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<Human> E => _modelBuilder.Entity<Human>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.Property(x => x.FullName).IsRequired().HasMaxLength(500);
            E.ToTable("Humans");
        }
    }
}
