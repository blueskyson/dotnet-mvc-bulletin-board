using Microsoft.AspNetCore.Mvc;
using BulletinBoard.Models;
using BulletinBoard.Infrasructure;
using BulletinBoard.Utils;

namespace BulletinBoard.Controllers;

[ServiceFilter(typeof(AuthorizationAttribute))]
public class PostController : Controller {
    private readonly IDbContext _dbContext;

    public PostController(IDbContext context) {
        _dbContext = context;
    }

    public async Task<IActionResult> Index(int? id) {
        if (id == null)
        {
            return NotFound();
        }
        Post? post = await _dbContext.GetPostAsync(id);
        List<ReplyWithDisplayName>? repliesList = await _dbContext.GetRepliesWithDisplayNames(id);
        var viewModel = new PostViewModel {
            Post = post,
            RepliesList = repliesList,
            DisplayName = _dbContext.GetDisplayNameById(post!.UserId),
        };
        return View(viewModel);        
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(int id, String NewReply) {
        int? userId = HttpContext.Session.GetInt32(SessionKeys.UserId);
        if (userId == null) {
            ViewData["Reply"] = "No such user. Log in again may fix the problem";
            return await Index(id);
        }

        if (string.IsNullOrEmpty(NewReply))
        {
            ViewData["Reply"] = "Reply can't be empty";
            return await Index(id);
        }

        Reply reply = new Reply {
            PostId = id,
            UserId = (int)userId,
            SubmitTime = DateTime.Now,
            Text = NewReply,
        };

        if (!_dbContext.CreateReply(reply)) {
            ViewData["Reply"] = "Error creating reply";
            return await Index(id);
        }

        return await Index(id);        
    }

    public IActionResult Create() {
        return View();        
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("Text")] Post post) {
        post.SubmitTime = DateTime.Now;
        int? userId = HttpContext.Session.GetInt32(SessionKeys.UserId);
        if (userId == null) {
            return View();
        }
        
        post.UserId = (int)userId;
        if (!_dbContext.CreatePost(post)) {
            return View();
        }
        return RedirectToAction("Index", "BulletinBoard");
    }
}