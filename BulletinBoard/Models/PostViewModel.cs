using BulletinBoard.Models.Entities;

namespace BulletinBoard.Models;

/// <summary>
/// Models needed by post page.
/// </summary>
public class PostViewModel
{
    public Post? Post { get; set; }
    public List<Reply>? Replies { get; set; }
}