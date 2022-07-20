using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BulletinBoard.Models;
using BulletinBoard.Infrasructure;

namespace BulletinBoard.Controllers;

[ServiceFilter(typeof(AuthorizationFilterAttribute))]
public class BulletinBoardController : Controller {
    private readonly IDbContext _dbContext;

    public BulletinBoardController(IDbContext context) {
        _dbContext = context;
    }
    public async Task<IActionResult> Index() {
        var viewModel = new BulletinBoardViewModel {
            PostsList = await _dbContext.GetAllPostsAsync(),
            UsersList = await _dbContext.GetAllUsersAsync(),
        };
        return View(viewModel);        
    }

    public IActionResult ChangeDisplayName() {
        return View();        
    }

    public IActionResult CreatePost() {
        return View();        
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreatePost([Bind("Text")] Post post) {
        Console.WriteLine(post.Text);
        post.SubmitTime = DateTime.Now;
        int? userId = HttpContext.Session.GetInt32("userid");
        if (userId == null) {
            return View();
        }
        
        post.UserId = (int)userId;
        Console.WriteLine("---- " + post.UserId);

        if (!_dbContext.CreatePost(post)) {
            return View();
        }
        return RedirectToAction(nameof(Index));
    }
}