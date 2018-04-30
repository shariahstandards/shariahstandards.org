using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class WordMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<Word> E => _modelBuilder.Entity<Word>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => new {x.SurahNumber,x.VerseNumber,x.WordNumber });
            E.HasRequired(x => x.Surah).WithMany(x => x.Words).HasForeignKey(x => x.SurahNumber).WillCascadeOnDelete(false);
            E.HasRequired(x => x.Verse).WithMany(x => x.Words).HasForeignKey(x => new { x.SurahNumber,x.VerseNumber }).WillCascadeOnDelete(false);
    }
    }
}
