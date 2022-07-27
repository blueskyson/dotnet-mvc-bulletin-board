using BulletinBoard.Models.Entities;
namespace BulletinBoard.Models.BusinessLogic;

/// <summary>
/// Business logic of LoginController.
/// </summary>
public interface ILoginLogic
{
    /// <summary>
    /// Validate the user.
    /// </summary>
    /// <param name="user">
    /// The user should contain Name and plain Password.
    /// This function will hash the password and do the match.
    /// </param>
    /// <returns>
    /// If the user's Name and Password are correct, 
    /// return the User entity from database. Otherwise, return null.
    /// </returns>
    Task<User?> UserExists(User user);
}