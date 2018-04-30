using StoredObjects.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoredObjects
{
    interface IMapping
    {
        void SetMapping(DbModelBuilder modelBuilder);
    }

    public class ApplicationContext:DbContext
    {
        public ApplicationContext():base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
          //modelBuilder.Conventions.Add(new AttributeToColumnAnnotationConvention<CaseSensitiveAttribute, bool>(
          //     "CaseSensitive",
          //     (property, attributes) => attributes.Single().IsEnabled));
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Auth0User>().HasKey(x => x.Id);
            modelBuilder.Entity<Auth0User>().Property(x => x.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Auth0User>().Property(x => x.PictureUrl).IsRequired().HasMaxLength(1000);
            var mappingClasses = this.GetType().Assembly.DefinedTypes.Where(t =>
             t.ImplementedInterfaces.Any(i => i == typeof(IMapping))).ToList();
            mappingClasses.Select(t =>
            {
                var constructorInfo = t.GetConstructor(new Type[0]);
                return constructorInfo?.Invoke(new object[0]);
            }).Where(c => c != null).Cast<IMapping>().ToList()
                .ForEach(m => m.SetMapping(modelBuilder));
        }
    }
}
