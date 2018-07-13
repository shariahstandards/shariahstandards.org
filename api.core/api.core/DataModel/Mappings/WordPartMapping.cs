using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
  public class WordPartMapping : IMapping
  {
    private ModelBuilder _modelBuilder;

    private EntityTypeBuilder<WordPart> E => _modelBuilder.Entity<WordPart>();

    public void SetMapping(ModelBuilder modelBuilder)
    {
      _modelBuilder = modelBuilder;
      E.HasKey(x => new { x.SurahNumber, x.VerseNumber, x.WordNumber, x.WordPartNumber });
      E.Property(x => x.Text).IsRequired().HasMaxLength(100);
      E.Property(x => x.WordPartTypeCode).IsRequired(false).HasMaxLength(10);
      E.HasOne(x => x.Word).WithMany(x => x.WordParts).HasForeignKey(x => new { x.SurahNumber, x.VerseNumber, x.WordNumber })
        .OnDelete(DeleteBehavior.Restrict);
      E.HasOne(x => x.Verse).WithMany(x => x.WordParts).HasForeignKey(x => new { x.SurahNumber, x.VerseNumber })
        .OnDelete(DeleteBehavior.Restrict);
      E.HasOne(x => x.Surah).WithMany(x => x.WordParts).HasForeignKey(x => new { x.SurahNumber })
        .OnDelete(DeleteBehavior.Restrict);
      E.HasOne(x => x.WordPartForm).WithMany(x => x.WordParts).HasForeignKey(x => new { x.Text })
        .OnDelete(DeleteBehavior.Restrict);
      E.HasOne(x => x.WordPartType).WithMany(x => x.WordParts).HasForeignKey(x => new { x.WordPartTypeCode })
       .OnDelete(DeleteBehavior.Restrict);
      E.HasOne(x => x.WordPartPositionType).WithMany(x => x.WordParts).HasForeignKey(x => new { x.WordPartPositionTypeCode })
      .OnDelete(DeleteBehavior.Restrict);

    }
  }
}
