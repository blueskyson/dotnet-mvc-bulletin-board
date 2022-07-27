using Microsoft.EntityFrameworkCore;
using BulletinBoard.Models.Entities;

namespace BulletinBoard.Models;

/// <summary>
/// An inheritance of DbContext.
/// </summary>
public class BulletinBoardContext : DbContext
{
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Post> Posts { get; set; } = default!;
    public DbSet<Reply> Replies { get; set; } = default!;

    /// <summary>
    /// Initialize database configuration.
    /// </summary>
    /// <param name="options"></param>
    public BulletinBoardContext(DbContextOptions<BulletinBoardContext> options)
        : base(options)
    {
    }
}