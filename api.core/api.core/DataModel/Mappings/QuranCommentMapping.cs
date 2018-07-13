using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class QuranCommentMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<QuranComment> E => _modelBuilder.Entity<QuranComment>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.Property(x => x.CommentText).IsRequired().HasMaxLength(4000);
            E.HasOne(x => x.Auth0User).WithMany(x => x.QuranComments).HasForeignKey(x => x.Auth0UserId);
        }
    }
}