using BulletinBoard.Models.Entities;
namespace BulletinBoard.Models.BusinessLogic;
public interface ILoginLogic
{
    Task<User?> UserExists(User user);
}