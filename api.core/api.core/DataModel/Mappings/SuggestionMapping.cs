using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class SuggestionMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<Suggestion> E => _modelBuilder.Entity<Suggestion>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.HasOne(x => x.AuthorMember).WithMany(x => x.Suggestions).HasForeignKey(x => x.AuthorMemberId);
            E.Property(x => x.ShortDescription).IsRequired().HasMaxLength(100);
            E.Property(x => x.FullText).IsRequired().HasMaxLength(4000);

        }
    }
}