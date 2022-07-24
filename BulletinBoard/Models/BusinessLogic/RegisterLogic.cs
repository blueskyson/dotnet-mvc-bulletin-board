using BulletinBoard.Models;
using BulletinBoard.Models.Entities;
namespace BulletinBoard.Models.BusinessLogic;
public class RegisterLogic : IRegisterLogic
{
    private IUnitOfWork _unitOfWork;

    public RegisterLogic(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }

    public bool UserNameExists(string name) {
        IQueryable<User>? userQuery = 
            from u in _unitOfWork.UserRepository.GetDbSet()
            where u.Name == name
            select u;
        return (userQuery.FirstOrDefault() != null);
    }

    public async Task<int> AddUserAsync(User user)
    {
        try
        {
            _unitOfWork.UserRepository.Add(user);
            return await _unitOfWork.SaveChangeAsync();
        }
        catch (Exception) { }
        return -1;
    }

}