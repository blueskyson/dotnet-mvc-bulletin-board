using System.Linq.Expressions;
namespace BulletinBoard.Models.Repositories;

public interface IGenericRepository<TEntity> where TEntity : class
{
        void Add(TEntity entity);
        Task<ICollection<TEntity>> GetAllAsync();
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate);
        void Remove(TEntity entity);
        void Update(TEntity entity);
}