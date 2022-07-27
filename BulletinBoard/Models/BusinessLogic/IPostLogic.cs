using BulletinBoard.Models.Entities;
namespace BulletinBoard.Models.BusinessLogic;

/// <summary>
/// Business logic of PostController.
/// </summary>
public interface IPostLogic
{
    /// <summary>
    /// Get a post by Id. The navigation property, User, should be joined. 
    /// </summary>
    /// <param name="postId"></param>
    /// <returns>
    /// A post entity of the postId.
    /// If there's no such post, return null.
    /// </returns>
    Task<Post?> GetPostByIdAsync(int? postId);

    /// <summary>
    /// Get all replies of a post with postId.
    /// </summary>
    /// <param name="postId"></param>
    /// <returns>A List of Reply.</returns>
    Task<List<Reply>?> GetRepliesByPostIdAsync(int? postId);

    /// <summary>
    /// Save a reply to database.
    /// </summary>
    /// <param name="reply"></param>
    /// <returns>If save successfully, return >= 0. Otherwise return -1.</returns>
    Task<int> AddReplyAsync(Reply reply);

    /// <summary>
    /// Save a post to database.
    /// </summary>
    /// <param name="post"></param>
    /// <returns>If save successfully, return >= 0. Otherwise return -1.</returns>
    Task<int> AddPostAsync(Post post);
}