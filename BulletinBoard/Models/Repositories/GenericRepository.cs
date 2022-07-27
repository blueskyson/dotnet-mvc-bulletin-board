using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulletinBoard.Models.Repositories;

/// <summary>
/// Repository pattern implementation.
/// </summary>
/// <typeparam name="TEntity">Database Entity</typeparam>
public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : class
{
    private readonly DbContext _context;

    /// <summary>
    /// Inject DbContext by DI Container.
    /// </summary>
    /// <param name="context">Access repositories.</param>
    public GenericRepository(DbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Add a new TEntity to database.
    /// </summary>
    /// <param name="entity"></param>
    public void Add(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("entity");
        }

        _context.Set<TEntity>().Add(entity);
    }

    /// <summary>
    /// Get all TEntity.
    /// </summary>
    /// <returns></returns>
    public async Task<List<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    /// <summary>
    /// Get the DbSet<TEntity> for querying.
    /// </summary>
    /// <returns></returns>
    public DbSet<TEntity> GetDbSet()
    {
        return _context.Set<TEntity>();
    }

    /// <summary>
    /// Get a Tentity by an expression.
    /// </summary>
    /// <returns>If not found, return null.</returns>
    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
    }

    /// <summary>
    /// Set a TEntity's state to EntityState.Deleted.
    /// </summary>
    /// <param name="entity"></param>
    public void Remove(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("entity");
        }

        _context.Entry(entity).State = EntityState.Deleted;
    }

    /// <summary>
    /// Set a TEntity's state to EntityState.Modified.
    /// </summary>
    /// <param name="entity"></param>
    public void Update(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("entity");
        }

        _context.Entry(entity).State = EntityState.Modified;
    }
}