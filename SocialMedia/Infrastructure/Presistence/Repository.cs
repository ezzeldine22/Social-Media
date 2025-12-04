using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Presistence
{
    public class Repository<TEntity> : IRepository <TEntity> where TEntity : class
    {
        private SocialMediaContext _context;
        private DbSet<TEntity> _entity;

        public Repository(SocialMediaContext context)
        {
            _context = context;
            _entity = _context.Set<TEntity>();
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {

            await _entity.AddAsync(entity);
            return entity;
        }

        public async Task<TEntity> ReadById(int RowID)
        {
            return await _entity.FindAsync(RowID);
        }
        public IQueryable<TEntity> ReadAll()
        {
            return _entity.AsNoTracking();
        }
        public void Update(TEntity entity)
        {
            _entity.Update(entity);
        }

        public async Task DeleteAsync(int entityID)
        {
            var oData = await _entity.FindAsync(entityID);
            _entity.Remove(oData);
        }
        public async Task  DeleteAsync(long entityID)
        {
            var oData = await _entity.FindAsync(entityID);
            _entity.Remove(oData);
        }
        


        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entity.FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _entity.Where(predicate);
        }
    }
}
