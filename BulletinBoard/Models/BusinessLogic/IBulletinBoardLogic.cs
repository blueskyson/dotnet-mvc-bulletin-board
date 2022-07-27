using BulletinBoard.Models.Entities;
namespace BulletinBoard.Models.BusinessLogic;

/// <summary>
/// Business logic of BulletinBoardController.
/// </summary>
public interface IBulletinBoardLogic
{
    /// <summary>
    /// Get all posts. The navigation property, User, should be joined.
    /// </summary>
    /// <returns>A List of all posts.</returns>
    Task<List<Post>> GetAllPostsAsync();
}