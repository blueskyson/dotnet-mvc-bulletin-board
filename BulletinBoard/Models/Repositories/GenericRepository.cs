using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulletinBoard.Models.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : class
{
    private readonly DbContext _context;

    public GenericRepository(DbContext context)
    {
        _context = context;
    }

    public void Add(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("entity");
        }

        _context.Set<TEntity>().Add(entity);
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public DbSet<TEntity> GetDbSet()
    {
        return _context.Set<TEntity>();
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
    }

    public void Remove(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("entity");
        }

        _context.Entry(entity).State = EntityState.Deleted;
    }

    public void Update(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("entity");
        }

        _context.Entry(entity).State = EntityState.Modified;
    }
}