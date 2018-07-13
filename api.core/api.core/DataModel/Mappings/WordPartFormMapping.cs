using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class WordPartFormMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<WordPartForm> E => _modelBuilder.Entity<WordPartForm>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Text);
            E.Property(x => x.Text).IsRequired().HasMaxLength(100);
        }
    }
}
