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

    public async Task<List<Reply>?> GetReplies(int? postId) {
        Post? post = await Posts.FirstOrDefaultAsync(p => p.Id == postId);
        if (post == null)
        {
            return null;
        }
        
        User? user = await Users.FirstOrDefaultAsync(u => u.Id == post!.UserId);
        post.User = user;

        var query = from r in Replies where r.PostId == postId
                    join u in Users on r.UserId equals u.Id
                    orderby r.SubmitTime 
                    select new Reply {
                        Id = r.Id,
                        SubmitTime = r.SubmitTime,
                        Text = r.Text,
                        User = u,
                    };
        List<Reply> repliesList = await query.ToListAsync();
        return repliesList;
    }

    public async Task<Post?> GetPostAsync(int? postId) {
        return await Posts.FirstOrDefaultAsync(p => p.Id == postId);
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