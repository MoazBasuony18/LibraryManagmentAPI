using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace LibraryManagmentAPI.Infrastructure.Repository
{
    public interface IRepository<TEntity>where TEntity : class
    {
        Task<IList<TEntity>>GetAll(Expression<Func<TEntity, bool>> predicate=null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<string> includes = null);
        Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null,
                                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                  Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                                  bool disableTracking = true,
                                                  bool ignoreQueryFilters = false);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate = null,List<string> includes = null);
        Task AddAsync(TEntity entity);
        Task RemoveAsync(int id);
        Task UpdateAsync(TEntity entity);
    }
}
