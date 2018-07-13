using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class MemberMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<Member> E => _modelBuilder.Entity<Member>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.Property(x => x.Introduction).IsRequired(false).HasMaxLength(4000);
            E.HasOne(x => x.Organisation).WithMany(x => x.Members).HasForeignKey(x => x.OrganisationId);
        }
    }
}