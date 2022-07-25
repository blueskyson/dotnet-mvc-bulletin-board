using BulletinBoard.Utils;
using BulletinBoard.Models.Entities;
namespace BulletinBoard.Models.BusinessLogic;
public class LoginLogic : ILoginLogic
{
    private IUnitOfWork _unitOfWork;
    private IHasher _hasher;

    public LoginLogic(IUnitOfWork unitOfWork, IHasher hasher)
    {
        _unitOfWork = unitOfWork;
        _hasher = hasher;
    }

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