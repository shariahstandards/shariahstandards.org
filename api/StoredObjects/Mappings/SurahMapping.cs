using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
  public class SurahMapping : IMapping
  {
    private DbModelBuilder _modelBuilder;

    private EntityTypeConfiguration<Surah> E => _modelBuilder.Entity<Surah>();

    public void SetMapping(DbModelBuilder modelBuilder)
    {
      _modelBuilder = modelBuilder;
      E.HasKey(x => x.SurahNumber);
      E.Property(x => x.SurahNumber).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
      E.Property(x => x.ArabicName).IsRequired().HasMaxLength(100);
      E.Property(x => x.EnglishName).IsRequired().HasMaxLength(100);
    }
  }
}
