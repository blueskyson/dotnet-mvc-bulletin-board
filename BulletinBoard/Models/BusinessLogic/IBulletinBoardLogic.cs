using BulletinBoard.Models.Entities;
namespace BulletinBoard.Models.BusinessLogic;
public interface IBulletinBoardLogic
{
    Task<List<Post>> GetAllPostsAsync();
}