using Microsoft.EntityFrameworkCore;
using BulletinBoard.Models.Entities;
using BulletinBoard.Models.Repositories;

namespace BulletinBoard.Models;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<User> UserRepository { get; }
    IGenericRepository<Post> PostRepository { get; }
    IGenericRepository<Reply> ReplyRepository { get; }
    DbContext Context { get; }
}