using Microsoft.EntityFrameworkCore;
using BulletinBoard.Models.Entities;
using BulletinBoard.Models.Repositories;

namespace BulletinBoard.Models;

public class UnitOfWork : IUnitOfWork
{
    public IGenericRepository<User> UserRepository { get; private set; }
    public IGenericRepository<Post> PostRepository { get; private set; }
    public IGenericRepository<Reply> ReplyRepository { get; private set; }
    public DbContext Context { get; private set; }

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

    public void Dispose()
    {
        Context.Dispose();
        GC.SuppressFinalize(this);
    }
}