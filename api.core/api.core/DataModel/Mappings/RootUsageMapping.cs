using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
  public class RootUsageMapping : IMapping
  {
    private ModelBuilder _modelBuilder;

    private EntityTypeBuilder<RootUsage> E => _modelBuilder.Entity<RootUsage>();

    public void SetMapping(ModelBuilder modelBuilder)
    {
      _modelBuilder = modelBuilder;
      E.HasKey(x => new { x.SurahNumber, x.VerseNumber, x.WordNumber, x.WordPartNumber });
      E.Property(x => x.RootText).IsRequired().HasMaxLength(20);
      E.HasOne(x => x.WordPart).WithOne(x => x.RootUsage).HasForeignKey<RootUsage>(x => new { x.SurahNumber, x.VerseNumber, x.WordNumber, x.WordPartNumber });
      E.HasOne(x => x.Root).WithMany(x => x.RootUsages).HasForeignKey(x => x.RootText);

    }
  }
}
