using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ILinqServiceDependencies
    {

    }
    public class LinqServiceDependencies : ILinqServiceDependencies
    {
    }
    public interface ILinqService
    {

        TSource SingleOrDefault<TSource>(IQueryable<TSource> source);
        TSource SingleOrDefault<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate);
        TSource SingleOrDefault<TSource>(IEnumerable<TSource> source);
        TSource SingleOrDefault<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate);
        int QueryableCount<TSource>(IQueryable<TSource> source);
        int EnumerableCount<TSource>(IEnumerable<TSource> source);
        int EnumerableSum<TSource>(IEnumerable<TSource> source, Func<TSource, int> predicate);

        TSource Single<TSource>(IQueryable<TSource> source);
        TSource Single<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate);
        IEnumerable<TResult> SelectEnumerable<TSource, TResult>(IEnumerable<TSource> source,
            Func<TSource, TResult> selector);
        IEnumerable<TResult> SelectIndexedEnumerable<TSource, TResult>(IEnumerable<TSource> source,
            Func<TSource, int, TResult> selector);
        IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector);
        IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector);
        List<TSource> EnumerableToList<TSource>(IEnumerable<TSource> source);
        TAccumulate Aggregate<TSource, TAccumulate>(IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func);
        IEnumerable<TResult> SelectMany<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector);
        IEnumerable<TSource> Where<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate);

    }
    public class LinqService: ILinqService
    {
        private readonly ILinqServiceDependencies _dependencies;

        public LinqService(ILinqServiceDependencies dependencies)
        {
            _dependencies = dependencies;
        }

        public TSource SingleOrDefault<TSource>(IQueryable<TSource> source)
        {
            return source.SingleOrDefault();

        }

        public TSource SingleOrDefault<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            return source.SingleOrDefault(predicate);
        }

        public TSource SingleOrDefault<TSource>(IEnumerable<TSource> source)
        {
            
            return source.SingleOrDefault();
        }

        public TSource SingleOrDefault<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return source.SingleOrDefault(predicate);
        }

        public int QueryableCount<TSource>(IQueryable<TSource> source)
        {
            return source.Count();
        }
        public int EnumerableCount<TSource>(IEnumerable<TSource> source)
        {
            return source.Count();
        }

        public int EnumerableSum<TSource>(IEnumerable<TSource> source, Func<TSource, int> predicate)
        {
            return source.Sum(predicate);
        }

        public TSource Single<TSource>(IQueryable<TSource> source)
        {
            return source.Single();
        }

        public TSource Single<TSource>(IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            return source.Single(predicate);
        }

        public IEnumerable<TResult> SelectEnumerable<TSource, TResult>(IEnumerable<TSource> source,
            Func<TSource, TResult> selector)
        {
            return source.Select(selector);
        }

        public IEnumerable<TResult> SelectIndexedEnumerable<TSource, TResult>(IEnumerable<TSource> source,
            Func<TSource, int, TResult> selector)
        {
            return source.Select(selector);
        }

        public IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.OrderBy(keySelector);
        }

        public IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.ThenBy(keySelector);
        }

        public List<TSource> EnumerableToList<TSource>(IEnumerable<TSource> source)
        {
            return source.ToList();
        }

        public TAccumulate Aggregate<TSource, TAccumulate>(IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
        {
            return source.Aggregate(seed, func);
        }

        public IEnumerable<TResult> SelectMany<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
        {
            return source.SelectMany(selector);
        }

        public IEnumerable<TSource> Where<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return source.Where(predicate);
        }
    }
}
