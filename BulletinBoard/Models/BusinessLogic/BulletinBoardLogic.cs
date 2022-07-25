using Microsoft.EntityFrameworkCore;
using BulletinBoard.Models.Entities;
namespace BulletinBoard.Models.BusinessLogic;
public class BulletinBoardLogic : IBulletinBoardLogic
{
    private IUnitOfWork _unitOfWork;

    public BulletinBoardLogic(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<List<Post>> GetAllPostsAsync()
    {
        var posts = from p in _unitOfWork.PostRepository.GetDbSet()
                    orderby p.SubmitTime
                    join u in _unitOfWork.UserRepository.GetDbSet() on p.UserId equals u.Id
                    select new Post
                    {
                        Id = p.Id,
                        SubmitTime = p.SubmitTime,
                        Text = p.Text,
                        User = u,
                    };

        return await posts.ToListAsync();
    }
}