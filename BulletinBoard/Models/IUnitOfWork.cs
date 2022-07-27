using Microsoft.EntityFrameworkCore;
using BulletinBoard.Models.Entities;
using BulletinBoard.Models.Repositories;

namespace BulletinBoard.Models;

/// <summary>
/// Unit of work interface.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    IGenericRepository<User> UserRepository { get; }
    IGenericRepository<Post> PostRepository { get; }
    IGenericRepository<Reply> ReplyRepository { get; }
    DbContext Context { get; }

    /// <summary>
    /// Write all changed entities to database.
    /// </summary>
    /// <returns>The number of changed states that written to database.</returns>
    Task<int> SaveChangeAsync();
}