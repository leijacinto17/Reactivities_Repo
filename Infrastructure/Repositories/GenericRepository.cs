using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DataContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetQueryable(
          Expression<Func<TEntity, bool>> filter = null,
          List<Expression<Func<TEntity, object>>>
              includeProperties = null,
          bool tracking = true)
        {
            IQueryable<TEntity> query = tracking ? _dbSet : _dbSet.AsNoTracking();

            if (includeProperties != null)
                includeProperties.ForEach(i => { query = query.Include(i); });

            if (filter != null)
                query = query.Where(filter);
            return query;
        }

        public virtual async Task<TEntity> Insert(TEntity entity)
        {
            var insert = await _dbSet.AddAsync(entity);
            return insert.Entity;
        }

        public virtual void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void SetRowVersion<T>(T entity, byte[] rowVersion) where T : class
        {
            _context.Entry(entity).OriginalValues["RowVersion"] = rowVersion;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && (_context != null))
                _context.Dispose();
        }
    }
}
