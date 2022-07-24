using BulletinBoard.Models.Entities;
namespace BulletinBoard.Models.BusinessLogic;
public interface IRegisterLogic
{
    bool UserNameExists(string name);
    Task<int> AddUserAsync(User user);
}