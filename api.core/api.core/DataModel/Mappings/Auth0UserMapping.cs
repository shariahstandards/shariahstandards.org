using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class Auth0UserMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<Auth0User> E => _modelBuilder.Entity<Auth0User>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x=>x.Id);
            E.Property(x => x.Id).HasMaxLength(200);
            E.Property(x => x.Name).IsRequired().HasMaxLength(200);
            E.Property(x => x.PictureUrl).IsRequired().HasMaxLength(1000);
        }
    }
}
