using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class ActionMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<Action> E => _modelBuilder.Entity<Action>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.Property(x => x.Description).IsRequired().HasMaxLength(500);
            E.HasOne(x => x.Organisation).WithMany(x => x.Actions).HasForeignKey(x => x.OrganisationId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
