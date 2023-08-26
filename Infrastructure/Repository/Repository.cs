using LibraryManagmentAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace LibraryManagmentAPI.Infrastructure.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext context;
        private readonly DbSet<TEntity> db;

        public Repository(AppDbContext context)
        {
            this.context = context;
            db=context.Set<TEntity>();
        }
        public async Task AddAsync(TEntity entity)
        {
            await db.AddAsync(entity);
        }

        public async Task<IList<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null, List<string> includes = null)
        {
            IQueryable<TEntity> query = db;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if(includes != null)
            {
                foreach (var includePropery in includes)
                {
                    query = query.Include(includePropery);
                }
            }
            if (orderBy != null)
            {
                query=orderBy(query);
            }
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<IList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true, bool ignoreQueryFilters = false)
        {
            IQueryable<TEntity> query = db;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate = null, List<string> includes = null)
        {
            IQueryable<TEntity>query=db;
            if(predicate != null)
            {
                query=query.Where(predicate);
            }
            if(includes!= null)
            {
                foreach(var includePropery in includes)
                {
                    query=query.Include(includePropery);
                }
            }
            return await query.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await db.FindAsync(id);
            if (entity != null)
            {
                db.Remove(entity);
            }
        }
        public async Task UpdateAsync(TEntity entity)
        {
             context.Entry(entity).State = EntityState.Modified;
        }
    }
}
