using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
  public class SurahMapping : IMapping
  {
    private ModelBuilder _modelBuilder;

    private EntityTypeBuilder<Surah> E => _modelBuilder.Entity<Surah>();

    public void SetMapping(ModelBuilder modelBuilder)
    {
      _modelBuilder = modelBuilder;
      E.HasKey(x => x.SurahNumber);
      E.Property(x => x.SurahNumber).ValueGeneratedNever();
      E.Property(x => x.ArabicName).IsRequired().HasMaxLength(100);
      E.Property(x => x.EnglishName).IsRequired().HasMaxLength(100);
    }
  }
}
