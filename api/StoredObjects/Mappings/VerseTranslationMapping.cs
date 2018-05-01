using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
  public class VerseTranslationMapping : IMapping
  {
    private DbModelBuilder _modelBuilder;

    private EntityTypeConfiguration<VerseTranslation> E => _modelBuilder.Entity<VerseTranslation>();

    public void SetMapping(DbModelBuilder modelBuilder)
    {
      _modelBuilder = modelBuilder;
      E.HasKey(x => new { x.SurahNumber, x.VerseNumber });
      E.HasRequired(x => x.Verse).WithOptional(x => x.Translation);
      E.Property(x => x.Text).IsRequired().HasMaxLength(4000);
    }
  }
}
