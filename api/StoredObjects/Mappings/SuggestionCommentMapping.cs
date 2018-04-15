using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class SuggestionCommentMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<SuggestionComment> E => _modelBuilder.Entity<SuggestionComment>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.Property(x => x.Comment).IsRequired().HasMaxLength(1000);
            E.HasRequired(x => x.Suggestion).WithMany(x => x.Comments).HasForeignKey(x => x.SuggestionId);
            E.HasRequired(x => x.CommentingMember).WithMany(x => x.SuggestionComments).HasForeignKey(x => x.CommentingMemberId).WillCascadeOnDelete(false);

        }
    }
}