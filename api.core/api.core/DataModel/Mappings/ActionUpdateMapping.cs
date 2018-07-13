using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class ActionUpdateMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<ActionUpdate> E => _modelBuilder.Entity<ActionUpdate>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.Property(x => x.UpdatedDescription).IsRequired().HasMaxLength(500);
            E.Property(x => x.HoursWorkedSincePreviousUpdate).HasColumnType("decimal(4,1)");
            E.HasOne(x => x.Action).WithMany(x => x.Updates).HasForeignKey(x => x.ActionId);
            E.HasOne(x => x.ResponsibleMember).WithMany(x => x.ActionUpdates).HasForeignKey(x => x.ActionId);
        }
    }
}
