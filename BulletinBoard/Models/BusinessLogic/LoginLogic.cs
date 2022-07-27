using BulletinBoard.Utils;
using BulletinBoard.Models.Entities;
namespace BulletinBoard.Models.BusinessLogic;

/// <summary>
/// Business logic implementation of LoginController.
/// </summary>
public class LoginLogic : ILoginLogic
{
    private IUnitOfWork _unitOfWork;
    private IHasher _hasher;

    /// <summary>
    /// Inject Unit of Work and Hasher by DI Container.
    /// </summary>
    /// <param name="unitOfWork">Access repositories.</param>
    /// <param name="hasher">Hash passwords.</param>
    public LoginLogic(IUnitOfWork unitOfWork, IHasher hasher)
    {
        _unitOfWork = unitOfWork;
        _hasher = hasher;
    }
    
    /// <summary>
    /// Validate the user.
    /// </summary>
    /// <param name="user">
    /// The user should contain Name and plain Password.
    /// This function will hash the password and do the match.
    /// </param>
    /// <returns>
    /// If the user's Name and Password are correct, 
    /// return the user entity from database. Otherwise, return null.
    /// </returns>
    public async Task<User?> UserExists(User formInputUser)
    {
        User? user = await _unitOfWork.UserRepository.GetAsync(
            u => (u.Name == formInputUser.Name)
        );

        if (user != null && ValidateSaltAndHash(formInputUser, user))
            return user;

        return null;
    }

    private bool ValidateSaltAndHash(User formInputUser, User user)
    {
        var hashed = _hasher.GenerateHashBase64(formInputUser.Password!, user.Salt!);
        return (hashed == user.Password);
    }
}