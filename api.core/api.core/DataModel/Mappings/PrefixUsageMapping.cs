using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
  public class PrefixUsageMapping : IMapping
  {
    private ModelBuilder _modelBuilder;

    private EntityTypeBuilder<PrefixUsage> E => _modelBuilder.Entity<PrefixUsage>();

    public void SetMapping(ModelBuilder modelBuilder)
    {
      _modelBuilder = modelBuilder;
      E.HasKey(x => new { x.SurahNumber, x.VerseNumber, x.WordNumber, x.WordPartNumber });
      E.Property(x => x.Text).IsRequired().HasMaxLength(20);
      E.HasOne(x => x.WordPart).WithOne(x => x.PrefixUsage).HasForeignKey<PrefixUsage>(x => new { x.SurahNumber, x.VerseNumber, x.WordNumber, x.WordPartNumber });
      E.HasOne(x => x.Prefix).WithMany(x => x.PrefixUsages).HasForeignKey(x => x.Text);
    }
  }
}
