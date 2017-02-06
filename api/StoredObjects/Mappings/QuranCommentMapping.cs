using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class QuranCommentMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<QuranComment> E => _modelBuilder.Entity<QuranComment>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.Property(x => x.CommentText).IsRequired().HasMaxLength(4000);
            E.HasRequired(x => x.Auth0User).WithMany(x => x.QuranComments).HasForeignKey(x => x.Auth0UserId);
        }
    }
}