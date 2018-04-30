using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoredObjects;

namespace Services
{
   
    public interface IStorageService
    {
        IDbSet<TEntity> SetOf<TEntity>() where TEntity:class;
        int SaveChanges();
    }
    public class StorageService: IStorageService,IDisposable
    {
        private readonly ApplicationContext _context;

        public StorageService()
        {
            _context = new ApplicationContext();
        }

        public IDbSet<TEntity> SetOf<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>();
        }

        public int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                ex.EntityValidationErrors.ToList().ForEach(e =>
                {
                    Console.WriteLine(e.Entry.Entity.ToString());
                    e.ValidationErrors.ToList().ForEach(ve =>
                    {
                        Console.WriteLine(ve.ErrorMessage);
                    });
                });
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    public void Dispose()
    {
      this._context.Dispose();
    }
  }
}
