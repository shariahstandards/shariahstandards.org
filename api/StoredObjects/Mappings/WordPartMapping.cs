using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
  public class WordPartMapping : IMapping
  {
    private DbModelBuilder _modelBuilder;

    private EntityTypeConfiguration<WordPart> E => _modelBuilder.Entity<WordPart>();

    public void SetMapping(DbModelBuilder modelBuilder)
    {
      _modelBuilder = modelBuilder;
      E.HasKey(x => new { x.SurahNumber, x.VerseNumber, x.WordNumber, x.WordPartNumber });
      E.Property(x => x.Text).IsRequired().HasMaxLength(100);
      E.Property(x => x.WordPartTypeCode).IsOptional().HasMaxLength(10);
      E.HasRequired(x => x.Word).WithMany(x => x.WordParts).HasForeignKey(x => new { x.SurahNumber, x.VerseNumber, x.WordNumber })
        .WillCascadeOnDelete(false);
      E.HasRequired(x => x.Verse).WithMany(x => x.WordParts).HasForeignKey(x => new { x.SurahNumber, x.VerseNumber })
        .WillCascadeOnDelete(false);
      E.HasRequired(x => x.Surah).WithMany(x => x.WordParts).HasForeignKey(x => new { x.SurahNumber })
        .WillCascadeOnDelete(false);
      E.HasRequired(x => x.WordPartForm).WithMany(x => x.WordParts).HasForeignKey(x => new { x.Text })
        .WillCascadeOnDelete(false);
      E.HasRequired(x => x.WordPartType).WithMany(x => x.WordParts).HasForeignKey(x => new { x.WordPartTypeCode })
       .WillCascadeOnDelete(false);
      E.HasRequired(x => x.WordPartPositionType).WithMany(x => x.WordParts).HasForeignKey(x => new { x.WordPartPositionTypeCode })
      .WillCascadeOnDelete(false);

    }
  }
}
