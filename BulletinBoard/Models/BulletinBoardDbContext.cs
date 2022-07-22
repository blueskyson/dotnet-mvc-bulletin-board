using Microsoft.EntityFrameworkCore;
using BulletinBoard.Models.Entities;

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
        var posts = from p in Posts orderby p.SubmitTime select p;
        List<Post> postsList = await posts.ToListAsync();
        return postsList;
    }

    public async Task<List<PostWithDisplayName>> GetAllPostsWithDisplayNamesAsync()
    {
        var query = from p in Posts orderby p.SubmitTime 
                    join u in Users on p.UserId equals u.Id
                    select new PostWithDisplayName {
                        Id = p.Id,
                        SubmitTime = p.SubmitTime,
                        Text = p.Text,
                        DisplayName = u.DisplayName,
                    };
        List<PostWithDisplayName> postsList = await query.ToListAsync();
        return postsList;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        var users = from u in Users select u;
        List<User> usersList = await users.ToListAsync();
        return usersList;
    }

    public bool CreatePost(Post post)
    {
        try
        {
            Add(post);
            SaveChanges();
            return true;
        }
        catch (Exception) { }
        return false;
    }

    public User? GetUserById(int id)
    {
        IQueryable<User>? userQuery = from u in Users
                                      where u.Id == id
                                      select u;
        return userQuery.FirstOrDefault();
    }

    public bool UpdateUser(User user)
    {
        try
        {
            Update(user);
            SaveChanges();
            return true;
        }
        catch (DbUpdateConcurrencyException) { }
        return false;
    }

    public async Task<List<ReplyWithDisplayName>?> GetRepliesWithDisplayNames(int? postId) {
        Post? post = await Posts.FirstOrDefaultAsync(p => p.Id == postId);
        if (post == null)
        {
            return null;
        }

        var query = from r in Replies where r.PostId == postId
                    join u in Users on r.UserId equals u.Id
                    orderby r.SubmitTime 
                    select new ReplyWithDisplayName {
                        Id = r.Id,
                        SubmitTime = r.SubmitTime,
                        Text = r.Text,
                        DisplayName = u.DisplayName,
                    };
        List<ReplyWithDisplayName> repliesList = await query.ToListAsync();
        return repliesList;
    }

    public async Task<Post?> GetPostAsync(int? postId) {
        return await Posts.FirstOrDefaultAsync(p => p.Id == postId);
    }

    public string GetDisplayNameById(int userId) {
        User? user = Users.FirstOrDefault(u => u.Id == userId);
        if (user == null) {
            return "";
        }
        return user.DisplayName!;
    }

    public bool CreateReply(Reply reply) {
        try
        {
            Add(reply);
            SaveChanges();
            return true;
        }
        catch (Exception) { }
        return false;
    }
}