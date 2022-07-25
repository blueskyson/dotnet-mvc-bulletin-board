using Microsoft.EntityFrameworkCore;
using BulletinBoard.Models.Entities;
namespace BulletinBoard.Models.BusinessLogic;
public class PostLogic : IPostLogic
{
    private IUnitOfWork _unitOfWork;

    public PostLogic(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Post?> GetPostByIdAsync(int? postId)
    {
        var query = from p in _unitOfWork.PostRepository.GetDbSet()
                    where p.Id == postId
                    join u in _unitOfWork.UserRepository.GetDbSet() on p.UserId equals u.Id
                    select new Post
                    {
                        Id = p.Id,
                        SubmitTime = p.SubmitTime,
                        Text = p.Text,
                        User = u,
                    };

        return await query.FirstOrDefaultAsync();
    }

    public async Task<List<Reply>?> GetRepliesByPostIdAsync(int? postId)
    {
        var query = from r in _unitOfWork.ReplyRepository.GetDbSet()
                    where r.PostId == postId
                    join u in _unitOfWork.UserRepository.GetDbSet() on r.UserId equals u.Id
                    orderby r.SubmitTime
                    select new Reply
                    {
                        Id = r.Id,
                        SubmitTime = r.SubmitTime,
                        Text = r.Text,
                        User = u,
                    };

        return await query.ToListAsync();
    }

    public async Task<int> AddReplyAsync(Reply reply)
    {
        try
        {
            _unitOfWork.ReplyRepository.Add(reply);
            return await _unitOfWork.SaveChangeAsync();
        }
        catch (Exception) { }

        return -1;
    }

    public async Task<int> AddPostAsync(Post post)
    {
        try
        {
            _unitOfWork.PostRepository.Add(post);
            return await _unitOfWork.SaveChangeAsync();
        }
        catch (Exception) { }

        return -1;
    }
}