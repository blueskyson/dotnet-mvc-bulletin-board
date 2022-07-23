using BulletinBoard.Models.Entities;

namespace BulletinBoard.Models;

public interface IDbContext {
    public User? UserExists(User user);
    public bool UserNameExists(string name);
    public bool CreateUser(User user);
    public Task<Post?> GetPostAsync(int? postId);
    public Task<List<Post>> GetAllPostsAsync();
    public Task<List<Reply>?> GetReplies(int? postId);
    public User? GetUserById(int id);
    public bool CreatePost(Post post);
    public bool CreateReply(Reply reply);
    public bool UpdateUser(User user);
}