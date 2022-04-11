using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BaarakuMiniBankAPIs.Middleware.Core.Repository
{

    public class Repository<T> : IRepository<T> where T : class
    {
        protected internal readonly DbContext _context;

        public Repository(DbContext dbContext)
        {
            _context = dbContext;
        }


        public async Task AddAsync(T model)
        {
            await _context.Set<T>().AddAsync(model).ConfigureAwait(false);
        }
        public async Task<T> GetAsync(long id)
        {
            return await _context.Set<T>().FindAsync(id).ConfigureAwait(false);
        }
        public async Task<T> GetAsync(
          Expression<Func<T, bool>> predicate,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
          params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            if (includes != null)
            {
                foreach (var property in includes)
                {
                    query = query.Include(property);
                }
            }
            if (orderBy != null) query = orderBy(query);
            return await query.SingleOrDefaultAsync(predicate).ConfigureAwait(false);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includes != null)
            {
                foreach (var property in includes)
                {
                    query = query.Include(property);
                }
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return await query.ToListAsync().ConfigureAwait(false);
        }

        public void Update(T model)
        {
            _context.Set<T>().Update(model);
        }
    }
}
