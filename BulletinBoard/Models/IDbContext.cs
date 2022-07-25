using BulletinBoard.Models.Entities;

namespace BulletinBoard.Models;

public interface IDbContext {
    public Task<Post?> GetPostAsync(int? postId);
    public Task<List<Reply>?> GetReplies(int? postId);
    public bool CreatePost(Post post);
    public bool CreateReply(Reply reply);
}