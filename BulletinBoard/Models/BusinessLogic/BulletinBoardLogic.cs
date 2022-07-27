using Microsoft.EntityFrameworkCore;
using BulletinBoard.Models.Entities;
namespace BulletinBoard.Models.BusinessLogic;

/// <summary>
/// Business logic implementation of BulletinBoardController.
/// </summary>
public class BulletinBoardLogic : IBulletinBoardLogic
{
    private IUnitOfWork _unitOfWork;

    /// <summary>
    /// Inject Unit of Work by DI Container.
    /// </summary>
    /// <param name="unitOfWork">Access repositories.</param>
    public BulletinBoardLogic(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Get all posts. The navigation property, User, should be joined.
    /// </summary>
    /// <returns>A List of all posts.</returns>
    public async Task<List<Post>> GetAllPostsAsync()
    {
        var posts = from p in _unitOfWork.PostRepository.GetDbSet()
                    orderby p.SubmitTime
                    join u in _unitOfWork.UserRepository.GetDbSet() on p.UserId equals u.Id
                    select new Post
                    {
                        Id = p.Id,
                        SubmitTime = p.SubmitTime,
                        Text = p.Text,
                        User = u,
                    };

        return await posts.ToListAsync();
    }
}