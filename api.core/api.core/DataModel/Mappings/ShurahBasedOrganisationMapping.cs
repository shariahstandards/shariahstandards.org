using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using  Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModel.Mappings
{
    public class ShurahBasedOrganisationMapping : IMapping
    {
        private ModelBuilder _modelBuilder;

        private EntityTypeBuilder<ShurahBasedOrganisation> E => _modelBuilder.Entity<ShurahBasedOrganisation>();

        public void SetMapping(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            E.HasKey(x => x.Id);
            E.Property(x => x.Name).IsRequired().HasMaxLength(250);
            E.Property(x => x.Description).IsRequired(false).HasMaxLength(4000);
            E.Property(x => x.UrlDomain).IsRequired(false).HasMaxLength(200);
            E.HasIndex(x => x.Name);
        }
    }
}
