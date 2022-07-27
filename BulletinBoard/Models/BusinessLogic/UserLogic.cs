using BulletinBoard.Models;
using BulletinBoard.Models.Entities;
namespace BulletinBoard.Models.BusinessLogic;

/// <summary>
/// Business logic implementation of UserController.
/// </summary>
public class UserLogic : IUserLogic
{
    private IUnitOfWork _unitOfWork;

    /// <summary>
    /// Inject Unit of Work and Hasher by DI Container.
    /// </summary>
    /// <param name="unitOfWork"></param>
    public UserLogic(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Check if a user with given Id already exists in database. 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>if user exists, reuturn the User entity. Otherwise, return null.</returns>
    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _unitOfWork.UserRepository.GetAsync(u => u.Id == id);
    }

    /// <summary>
    /// Update the user in the database.
    /// </summary>
    /// <param name="user"></param>
    /// <returns>Return true if update successfully. Otherwise, return false.</returns>
    public async Task<bool> UpdateUserAsync(User user)
    {
        try
        {
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        catch (Exception) { }

        return false;
    }
}