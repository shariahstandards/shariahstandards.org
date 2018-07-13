using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
  public class VerseTranslationMapping : IMapping
  {
    private ModelBuilder _modelBuilder;

    private EntityTypeBuilder<VerseTranslation> E => _modelBuilder.Entity<VerseTranslation>();

    public void SetMapping(ModelBuilder modelBuilder)
    {
      _modelBuilder = modelBuilder;
      E.HasKey(x => new { x.SurahNumber, x.VerseNumber });
      E.HasOne(x => x.Verse).WithOne(x => x.Translation).HasForeignKey<VerseTranslation>(x => new { x.SurahNumber, x.VerseNumber });
      E.Property(x => x.Text).IsRequired().HasMaxLength(4000);
    }
  }
}
