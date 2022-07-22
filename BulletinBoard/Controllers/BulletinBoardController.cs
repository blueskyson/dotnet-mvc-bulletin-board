using Microsoft.AspNetCore.Mvc;
using BulletinBoard.Models;
using BulletinBoard.Infrasructure;
using BulletinBoard.Utils;

namespace BulletinBoard.Controllers;

[ServiceFilter(typeof(AuthorizationFilterAttribute))]
public class BulletinBoardController : Controller {
    private readonly IDbContext _dbContext;

    public BulletinBoardController(IDbContext context) {
        _dbContext = context;
    }
    public async Task<IActionResult> Index() {
        List<PostWithDisplayName> viewModel = await _dbContext.GetAllPostsWithDisplayNamesAsync();
        return View(viewModel);        
    }

    public IActionResult ChangeDisplayName() {
        return View();        
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [TypeFilter(typeof(FormValidationAttribute), Arguments = new object[] {"DisplayName"})]
    public IActionResult ChangeDisplayName(String DisplayName) {
        int? userId = HttpContext.Session.GetInt32(SessionKeys.UserId);
        if (userId == null) {
            ViewData["DisplayName"] = "No such user. Log in again may fix the problem";
            return View();
        }

        User? currentUser = _dbContext.GetUserById((int)userId!);
        currentUser!.DisplayName = DisplayName;
        if (!_dbContext.UpdateUser(currentUser)) {
            ViewData["DisplayName"] = "Error changing name";
            return View();
        }
        HttpContext.Session.SetString(SessionKeys.DisplayName, DisplayName);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult CreatePost() {
        return View();        
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreatePost([Bind("Text")] Post post) {
        post.SubmitTime = DateTime.Now;
        int? userId = HttpContext.Session.GetInt32(SessionKeys.UserId);
        if (userId == null) {
            return View();
        }
        
        post.UserId = (int)userId;
        if (!_dbContext.CreatePost(post)) {
            return View();
        }
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Post(int? id) {
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
    public async Task<IActionResult> Post(int id, String newReply) {
        int? userId = HttpContext.Session.GetInt32(SessionKeys.UserId);
        if (userId == null) {
            ViewData["ReplyStatus"] = "No such user. Log in again may fix the problem";
            return await Post(id);
        }

        if (string.IsNullOrEmpty(newReply))
        {
            ViewData["ReplyStatus"] = "Reply can't be empty";
            return await Post(id);
        }

        Reply reply = new Reply {
            PostId = id,
            UserId = (int)userId,
            SubmitTime = DateTime.Now,
            Text = newReply,
        };

        if (!_dbContext.CreateReply(reply)) {
            ViewData["ReplyStatus"] = "Error creating reply";
            return await Post(id);
        }

        return await Post(id);        
    }
}