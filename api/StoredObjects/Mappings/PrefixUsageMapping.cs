using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
  public class PrefixUsageMapping : IMapping
  {
    private DbModelBuilder _modelBuilder;

    private EntityTypeConfiguration<PrefixUsage> E => _modelBuilder.Entity<PrefixUsage>();

    public void SetMapping(DbModelBuilder modelBuilder)
    {
      _modelBuilder = modelBuilder;
      E.HasKey(x => new { x.SurahNumber, x.VerseNumber, x.WordNumber, x.WordPartNumber });
      E.Property(x => x.Text).IsRequired().HasMaxLength(20);
      E.HasRequired(x => x.WordPart).WithOptional(x => x.PrefixUsage);
      E.HasRequired(x => x.Prefix).WithMany(x => x.PrefixUsages).HasForeignKey(x => x.Text);
    }
  }
}
