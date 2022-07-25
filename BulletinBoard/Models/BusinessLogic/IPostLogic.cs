using BulletinBoard.Models.Entities;
namespace BulletinBoard.Models.BusinessLogic;
public interface IPostLogic
{
    Task<Post?> GetPostByIdAsync(int? postId);
    Task<List<Reply>?> GetRepliesByPostIdAsync(int? postId);
    Task<int> AddReplyAsync(Reply reply);
    Task<int> AddPostAsync(Post post);
}