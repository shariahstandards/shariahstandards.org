using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations;

namespace Services
{

  public interface IStorageService
  {
    DbSet<TEntity> SetOf<TEntity>() where TEntity : class;
    int SaveChanges();
  }
  public class StorageService : IStorageService, IDisposable
  {

    private ApplicationContext _context;
    public StorageService(ApplicationContext context)
    {
      _context = context;
    }

    public DbSet<TEntity> SetOf<TEntity>() where TEntity : class
    {
      return _context.Set<TEntity>();
    }

    public int SaveChanges()
    {
      var entities = _context.ChangeTracker.Entries().Where(e =>
                     e.State == EntityState.Added
                         || e.State == EntityState.Modified).Select(e => e.Entity);
      var validationExceptions = new List<string>();
      foreach (var entity in entities)
      {
        var validationContext = new ValidationContext(entity);
        try
        {
          Validator.ValidateObject(entity, validationContext);
        }
        catch (ValidationException ex)
        {

          validationExceptions.Add(entity.ToString());
          validationExceptions.Add(ex.Message);
          validationExceptions.Add("------------");
        }
      }
      if (validationExceptions.Any())
      {
        throw new Exception("validation errors during database save : \n"+string.Join("\n",validationExceptions));
      }
      return _context.SaveChanges();
      
    }

    public void Dispose()
    {
      this._context.Dispose();
    }
  }
}
