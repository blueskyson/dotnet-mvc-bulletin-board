using BulletinBoard.Models;
using BulletinBoard.Models.Entities;
namespace BulletinBoard.Models.BusinessLogic;
public class UserLogic : IUserLogic
{
    private IUnitOfWork _unitOfWork;

    public UserLogic(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _unitOfWork.UserRepository.GetAsync(u => u.Id == id);
    }

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