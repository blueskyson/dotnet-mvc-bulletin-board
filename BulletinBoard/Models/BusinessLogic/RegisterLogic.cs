using BulletinBoard.Utils;
using BulletinBoard.Models.Entities;
namespace BulletinBoard.Models.BusinessLogic;

/// <summary>
/// Business logic implementation of RegisterController.
/// </summary>
public class RegisterLogic : IRegisterLogic
{
    private IUnitOfWork _unitOfWork;
    private IHasher _hasher;

    /// <summary>
    /// Inject Unit of Work and Hasher by DI Container.
    /// </summary>
    /// <param name="unitOfWork">Access repositories.</param>
    /// <param name="hasher">Hash passwords.</param>
    public RegisterLogic(IUnitOfWork unitOfWork, IHasher hasher)
    {
        _unitOfWork = unitOfWork;
        _hasher = hasher;
    }

    /// <summary>
    /// Check if a user with given Name already exists in database. 
    /// </summary>
    /// <param name="name"></param>
    /// <returns>if user exists, reuturn true. Otherwise, return false.</returns>
    public bool UserNameExists(string name)
    {
        IQueryable<User>? userQuery =
            from u in _unitOfWork.UserRepository.GetDbSet()
            where u.Name == name
            select u;

        return (userQuery.FirstOrDefault() != null);
    }

    /// <summary>
    /// Save a user to database.
    /// </summary>
    /// <param name="user"></param>
    /// <returns>If save successfully, return >= 0. Otherwise return -1.</returns>
    public async Task<int> AddUserAsync(User user)
    {
        try
        {
            ProcessSaltAndHash(user);
            _unitOfWork.UserRepository.Add(user);
            return await _unitOfWork.SaveChangeAsync();
        }
        catch (Exception) { }

        return -1;
    }

    private void ProcessSaltAndHash(User user)
    {
        user.Salt = _hasher.GenerateSaltBase64();
        user.Password = _hasher.GenerateHashBase64(user.Password!, user.Salt);
    }

}