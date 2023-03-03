using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IGenericRepository
    {
        void SetRowVersion<T>(T entity, byte[] rowVersion) where T : class;
    }

    /// <summary> Interface for the Generic Repository </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IGenericRepository<TEntity> : IGenericRepository, IDisposable where TEntity : class
    {
        IQueryable<TEntity> GetQueryable(
          Expression<Func<TEntity, bool>> filter = null,
          List<Expression<Func<TEntity, object>>>
              includeProperties = null,
          bool tracking = true);

        Task<TEntity> Insert(TEntity entity);
        void Delete(TEntity entity);
    }
}
