using BulletinBoard.Models.Entities;
namespace BulletinBoard.Models.BusinessLogic;

/// <summary>
/// Business logic of RegisterController.
/// </summary>
public interface IRegisterLogic
{
    /// <summary>
    /// Check if a user with given Name already exists in database. 
    /// </summary>
    /// <param name="name"></param>
    /// <returns>if user exists, reuturn true. Otherwise, return false.</returns>
    bool UserNameExists(string name);

    /// <summary>
    /// Save a user to database.
    /// </summary>
    /// <param name="user"></param>
    /// <returns>If save successfully, return >= 0. Otherwise return -1.</returns>
    Task<int> AddUserAsync(User user);
}