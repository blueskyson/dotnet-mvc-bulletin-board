using BulletinBoard.Models.Entities;

namespace BulletinBoard.Models;

public interface IDbContext {
    public Task<Post?> GetPostAsync(int? postId);
    public Task<List<Reply>?> GetReplies(int? postId);
    public User? GetUserById(int id);
    public bool CreatePost(Post post);
    public bool CreateReply(Reply reply);
    public bool UpdateUser(User user);
}