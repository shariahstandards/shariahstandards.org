using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class SuggestionMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<Suggestion> E => _modelBuilder.Entity<Suggestion>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.HasRequired(x => x.AuthorMember).WithMany(x => x.Suggestions).HasForeignKey(x => x.AuthorMemberId);
            E.Property(x => x.ShortDescription).IsRequired().HasMaxLength(100);
            E.Property(x => x.FullText).IsRequired().HasMaxLength(4000);

        }
    }
}