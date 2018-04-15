using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoredObjects.Mappings
{
    public class DelegatedPermissionMapping : IMapping
    {
        private DbModelBuilder _modelBuilder;

        private EntityTypeConfiguration<DelegatedPermission> E => _modelBuilder.Entity<DelegatedPermission>();

        public void SetMapping(DbModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.HasRequired(x => x.Member).WithMany(x => x.DelegatedPermissions).HasForeignKey(x => x.MemberId);
        }
    }
}