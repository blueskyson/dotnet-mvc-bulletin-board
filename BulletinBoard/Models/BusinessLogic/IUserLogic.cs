using BulletinBoard.Models.Entities;
namespace BulletinBoard.Models.BusinessLogic;
public interface IUserLogic
{
    Task<User?> GetUserByIdAsync(int id);
    Task<bool> UpdateUserAsync(User user);
}