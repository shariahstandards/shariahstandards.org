using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class DelegatedPermissionMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<DelegatedPermission> E => _modelBuilder.Entity<DelegatedPermission>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.HasOne(x => x.Member).WithMany(x => x.DelegatedPermissions).HasForeignKey(x => x.MemberId);
        }
    }
}