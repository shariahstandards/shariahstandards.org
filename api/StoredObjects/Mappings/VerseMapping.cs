using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class VerseMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<Verse> E => _modelBuilder.Entity<Verse>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => new { x.SurahNumber,x.VerseNumber });
            E.HasRequired(x => x.Surah).WithMany(x => x.Verses).HasForeignKey(x => x.SurahNumber).WillCascadeOnDelete(false);
        }
    }
}
