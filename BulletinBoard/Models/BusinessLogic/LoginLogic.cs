using BulletinBoard.Models;
using BulletinBoard.Models.Entities;
namespace BulletinBoard.Models.BusinessLogic;
public class LoginLogic : ILoginLogic
{
    private IUnitOfWork _unitOfWork;

    public LoginLogic(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<User?> UserExists(User user)
    {
        return await _unitOfWork.UserRepository.GetAsync(
            u => (u.Name == user.Name && u.Password == user.Password)
        );
    }
}