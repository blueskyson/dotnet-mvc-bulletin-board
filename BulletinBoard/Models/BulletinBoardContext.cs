using Microsoft.EntityFrameworkCore;
using BulletinBoard.Models.Entities;

namespace BulletinBoard.Models;

public class BulletinBoardContext : DbContext {
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Post> Posts { get; set; } = default!;
    public DbSet<Reply> Replies { get; set; } = default!;

    public BulletinBoardContext(DbContextOptions<BulletinBoardContext> options)
        : base(options)
    {
    } 
}