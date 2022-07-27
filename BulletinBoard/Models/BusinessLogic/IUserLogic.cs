using BulletinBoard.Models.Entities;
namespace BulletinBoard.Models.BusinessLogic;

/// <summary>
/// Business logic of UserController.
/// </summary>
public interface IUserLogic
{
    /// <summary>
    /// Check if a user with given Id already exists in database. 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>if user exists, reuturn the User entity. Otherwise, return null.</returns>
    Task<User?> GetUserByIdAsync(int id);

    /// <summary>
    /// Update the user in the database.
    /// </summary>
    /// <param name="user"></param>
    /// <returns>Return true if update successfully. Otherwise, return false.</returns>
    Task<bool> UpdateUserAsync(User user);
}