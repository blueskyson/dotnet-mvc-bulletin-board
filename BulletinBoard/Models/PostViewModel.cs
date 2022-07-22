using BulletinBoard.Models.Entities;

namespace BulletinBoard.Models;

public class PostViewModel {
    public Post? Post { get; set; }
    public List<ReplyWithDisplayName>? RepliesList { get; set; }
    public string? DisplayName { get; set; }
}