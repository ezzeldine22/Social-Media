using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> ReadById(int RowID);
        IQueryable<TEntity> ReadAll();

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        void Update(TEntity entity);
        Task DeleteAsync(int entityID);
        Task DeleteAsync(long entityID);

        Task SaveChanges();

    }
}
