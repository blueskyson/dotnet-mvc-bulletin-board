using Microsoft.EntityFrameworkCore;
using BulletinBoard.Models.Entities;
namespace BulletinBoard.Models.BusinessLogic;

/// <summary>
/// Business logic implementation of PostController.
/// </summary>
public class PostLogic : IPostLogic
{
    private IUnitOfWork _unitOfWork;

    /// <summary>
    /// Inject Unit of Work by DI Container.
    /// </summary>
    /// <param name="unitOfWork">Access repositories.</param>
    public PostLogic(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Get a post by Id. The navigation property, User, should be joined. 
    /// </summary>
    /// <param name="postId"></param>
    /// <returns>
    /// A post entity of the postId.
    /// If there's no such post, return null.
    /// </returns>
    public async Task<Post?> GetPostByIdAsync(int? postId)
    {
        var query = from p in _unitOfWork.PostRepository.GetDbSet()
                    where p.Id == postId
                    join u in _unitOfWork.UserRepository.GetDbSet() on p.UserId equals u.Id
                    select new Post
                    {
                        Id = p.Id,
                        SubmitTime = p.SubmitTime,
                        Text = p.Text,
                        User = u,
                    };

        return await query.FirstOrDefaultAsync();
    }

    /// <summary>
    /// Get all replies of a post with postId.
    /// </summary>
    /// <param name="postId"></param>
    /// <returns>A List of Reply.</returns>
    public async Task<List<Reply>?> GetRepliesByPostIdAsync(int? postId)
    {
        var query = from r in _unitOfWork.ReplyRepository.GetDbSet()
                    where r.PostId == postId
                    join u in _unitOfWork.UserRepository.GetDbSet() on r.UserId equals u.Id
                    orderby r.SubmitTime
                    select new Reply
                    {
                        Id = r.Id,
                        SubmitTime = r.SubmitTime,
                        Text = r.Text,
                        User = u,
                    };

        return await query.ToListAsync();
    }

    /// <summary>
    /// Save a reply to database. 
    /// </summary>
    /// <param name="reply"></param>
    /// <returns>If save successfully, return >= 0. Otherwise return -1.</returns>
    public async Task<int> AddReplyAsync(Reply reply)
    {
        try
        {
            _unitOfWork.ReplyRepository.Add(reply);
            return await _unitOfWork.SaveChangeAsync();
        }
        catch (Exception) { }

        return -1;
    }

    /// <summary>
    /// Save a post to database. 
    /// </summary>
    /// <param name="post"></param>
    /// <returns>If save successfully, return >= 0. Otherwise return -1.</returns>
    public async Task<int> AddPostAsync(Post post)
    {
        try
        {
            _unitOfWork.PostRepository.Add(post);
            return await _unitOfWork.SaveChangeAsync();
        }
        catch (Exception) { }

        return -1;
    }
}