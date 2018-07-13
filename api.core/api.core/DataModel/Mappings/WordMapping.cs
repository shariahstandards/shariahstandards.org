using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class WordMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<Word> E => _modelBuilder.Entity<Word>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => new {x.SurahNumber,x.VerseNumber,x.WordNumber });
            E.HasOne(x => x.Surah).WithMany(x => x.Words).HasForeignKey(x => x.SurahNumber).OnDelete(DeleteBehavior.Restrict);
            E.HasOne(x => x.Verse).WithMany(x => x.Words).HasForeignKey(x => new { x.SurahNumber,x.VerseNumber }).OnDelete(DeleteBehavior.Restrict);
    }
    }
}
