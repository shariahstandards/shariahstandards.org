using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class VerseMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<Verse> E => _modelBuilder.Entity<Verse>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => new { x.SurahNumber,x.VerseNumber });
            E.HasOne(x => x.Surah).WithMany(x => x.Verses).HasForeignKey(x => x.SurahNumber).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
