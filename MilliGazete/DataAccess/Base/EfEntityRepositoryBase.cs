using DataAccess.Concrete.EntityFramework.Contexts;
using Entity.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Base
{
    public class EfEntityRepositoryBase<TEntity> : IEntityRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly MilliGazeteDbContext _context;
        public EfEntityRepositoryBase(MilliGazeteDbContext context)
        {
            _context = context;
        }
        public MilliGazeteDbContext Db
        {
            get => _context;
        }
        public async Task<int> Add(TEntity entity)
        {
            var addedEntity = _context.Entry(entity);
            addedEntity.State = EntityState.Added;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(TEntity entity)
        {
            var deletedEntity = _context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveAll()
        {
            var data = _context.Set<TEntity>();
            if (data != null && data.Any())
            {
                _context.Set<TEntity>().RemoveRange(data);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> RemoveRange(List<TEntity> entities)
        {

            if (entities != null)
            {
                _context.Set<TEntity>().RemoveRange(entities);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? await _context.Set<TEntity>().FirstOrDefaultAsync() :
            await _context.Set<TEntity>().FirstOrDefaultAsync(filter);
        }

        public int Count(Expression<Func<TEntity, bool>> filter)
        {
            return _context.Set<TEntity>().Count(filter);
        }

        public IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ?
                _context.Set<TEntity>().AsNoTracking().AsQueryable() :
                _context.Set<TEntity>().AsNoTracking().AsQueryable().Where(filter);
        }

        public async Task<int> Update(TEntity entity)
        {
            var updatedEntity = _context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
    }
}
