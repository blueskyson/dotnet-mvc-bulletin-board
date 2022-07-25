using BulletinBoard.Models.Entities;

namespace BulletinBoard.Models;

public class PostViewModel
{
    public Post? Post { get; set; }
    public List<Reply>? Replies { get; set; }
}