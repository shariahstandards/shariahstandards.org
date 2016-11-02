using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class ActionUpdateMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<ActionUpdate> E => _modelBuilder.Entity<ActionUpdate>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.Property(x => x.UpdatedDescription).IsRequired().HasMaxLength(500);
            E.HasRequired(x => x.Action).WithMany(x => x.Updates).HasForeignKey(x => x.ActionId);
            E.HasRequired(x => x.ResponsibleMember).WithMany(x => x.ActionUpdates).HasForeignKey(x => x.ActionId);
        }
    }
}