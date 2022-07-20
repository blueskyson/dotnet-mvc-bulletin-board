using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulletinBoard.Models;

public class BulletinBoardViewModel
{
    public List<Post>? PostsList { get; set; }
    public List<User>? UsersList { get; set; }
}
