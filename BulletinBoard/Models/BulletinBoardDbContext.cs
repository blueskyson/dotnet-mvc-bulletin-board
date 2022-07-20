using Microsoft.EntityFrameworkCore;
using System;
namespace BulletinBoard.Models;

public class BulletinBoardDbContext : DbContext, IDbContext
{
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Post> Posts { get; set; } = default!;
    public DbSet<Reply> Replies { get; set; } = default!;

    public BulletinBoardDbContext(DbContextOptions<BulletinBoardDbContext> options)
        : base(options)
    {
    }

    public bool UserNameExists(string name)
    {
        IQueryable<User>? userQuery = from u in Users
                                      where u.Name == name
                                      select u;
        return (userQuery.FirstOrDefault() != null);
    }

    public User? UserExists(User user)
    {
        IQueryable<User>? userQuery = from u in Users
                                      where u.Name == user.Name && u.Password == user.Password
                                      select u;
        return userQuery.FirstOrDefault();
    }

    public bool CreateUser(User user)
    {
        try
        {
            Add(user);
            SaveChanges();
            return true;
        }
        catch (Exception) { }
        return false;
    }

    public async Task<List<Post>> GetAllPostsAsync()
    {
        var posts = from p in Posts select p;
        List<Post> postsList = await posts.ToListAsync();
        return postsList;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        var users = from u in Users select u;
        List<User> usersList = await users.ToListAsync();
        return usersList;
    }
}