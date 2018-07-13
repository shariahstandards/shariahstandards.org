using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class SuggestionCommentMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<SuggestionComment> E => _modelBuilder.Entity<SuggestionComment>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.Property(x => x.Comment).IsRequired().HasMaxLength(1000);
            E.HasOne(x => x.Suggestion).WithMany(x => x.Comments).HasForeignKey(x => x.SuggestionId);
            E.HasOne(x => x.CommentingMember).WithMany(x => x.SuggestionComments).HasForeignKey(x => x.CommentingMemberId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}