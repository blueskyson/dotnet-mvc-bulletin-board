using Microsoft.EntityFrameworkCore;
using BulletinBoard.Models.Entities;
using BulletinBoard.Models.Repositories;

namespace BulletinBoard.Models;

/// <summary>
/// Unit of work implementation.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    public IGenericRepository<User> UserRepository { get; private set; }
    public IGenericRepository<Post> PostRepository { get; private set; }
    public IGenericRepository<Reply> ReplyRepository { get; private set; }
    public DbContext Context { get; private set; }

    /// <summary>
    /// Inject all repositories by DI Container.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="userRepository"></param>
    /// <param name="postRepository"></param>
    /// <param name="replyRepository"></param>
    public UnitOfWork(
        DbContext context,
        IGenericRepository<User> userRepository,
        IGenericRepository<Post> postRepository,
        IGenericRepository<Reply> replyRepository)
    {
        Context = context;
        UserRepository = userRepository;
        PostRepository = postRepository;
        ReplyRepository = replyRepository;
    }

    /// <summary>
    /// Write all changed entities to database.
    /// </summary>
    /// <returns>The number of changed states that written to database.</returns>
    public async Task<int> SaveChangeAsync()
    {
        return await Context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Context.Dispose();
        GC.SuppressFinalize(this);
    }
}