using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
  public class UnmodifiedWordUsageMapping : IMapping
  {
    private DbModelBuilder _modelBuilder;

    private EntityTypeConfiguration<UnmodifiedWordPartUsage> E => _modelBuilder.Entity<UnmodifiedWordPartUsage>();

    public void SetMapping(DbModelBuilder modelBuilder)
    {
      _modelBuilder = modelBuilder;
      E.HasKey(x => new { x.SurahNumber, x.VerseNumber, x.WordNumber, x.WordPartNumber });
      E.Property(x => x.Text).IsRequired().HasMaxLength(20);
      E.HasRequired(x => x.WordPart).WithOptional(x => x.UnmodifiedWordPartUsage);
      E.HasRequired(x => x.UnmodifiedWord).WithMany(x => x.Usages).HasForeignKey(x => x.Text);

    }
  }
}
