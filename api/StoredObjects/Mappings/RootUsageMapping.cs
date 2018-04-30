using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
  public class RootUsageMapping : IMapping
  {
    private DbModelBuilder _modelBuilder;

    private EntityTypeConfiguration<RootUsage> E => _modelBuilder.Entity<RootUsage>();

    public void SetMapping(DbModelBuilder modelBuilder)
    {
      _modelBuilder = modelBuilder;
      E.HasKey(x => new { x.SurahNumber, x.VerseNumber, x.WordNumber, x.WordPartNumber });
      E.Property(x => x.RootText).IsRequired().HasMaxLength(20);
      E.HasRequired(x => x.WordPart).WithOptional(x => x.RootUsage);
      E.HasRequired(x => x.Root).WithMany(x => x.RootUsages).HasForeignKey(x => x.RootText);

    }
  }
}
