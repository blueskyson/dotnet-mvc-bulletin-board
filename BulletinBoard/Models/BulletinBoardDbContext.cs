using Microsoft.EntityFrameworkCore;
using System;
namespace BulletinBoard.Models;

public class BulletinBoardDbContext : DbContext {
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Post> Posts { get; set; } = default!;
    public DbSet<Reply> Replies { get; set; } = default!;

    public BulletinBoardDbContext (DbContextOptions<BulletinBoardDbContext> options)
        : base(options)
    {
    }
}