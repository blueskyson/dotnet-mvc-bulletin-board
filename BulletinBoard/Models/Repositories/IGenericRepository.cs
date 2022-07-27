using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace BulletinBoard.Models.Repositories;

/// <summary>
/// Repository pattern ineterface.
/// </summary>
/// <typeparam name="TEntity">Database Entity</typeparam>
public interface IGenericRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Add a new TEntity to database.
    /// </summary>
    /// <param name="entity"></param>
    void Add(TEntity entity);

    /// <summary>
    /// Get a list of all TEntity.
    /// </summary>
    /// <returns></returns>
    Task<List<TEntity>> GetAllAsync();

    /// <summary>
    /// Get a Tentity by an expression.
    /// </summary>
    /// <returns>If not found, return null.</returns>
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Set a TEntity's state to EntityState.Deleted.
    /// </summary>
    /// <param name="entity"></param>
    void Remove(TEntity entity);

    /// <summary>
    /// Set a TEntity's state to EntityState.Modified.
    /// </summary>
    /// <param name="entity"></param>
    void Update(TEntity entity);

    /// <summary>
    /// Get the DbSet<TEntity> for querying.
    /// </summary>
    /// <returns></returns>
    DbSet<TEntity> GetDbSet();

}