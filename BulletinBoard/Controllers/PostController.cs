using Microsoft.AspNetCore.Mvc;
using BulletinBoard.Models;
using BulletinBoard.Models.Entities;
using BulletinBoard.Models.BusinessLogic;
using BulletinBoard.Infrasructure;
using BulletinBoard.Utils;

namespace BulletinBoard.Controllers;

[ServiceFilter(typeof(AuthorizationAttribute))]
public class PostController : Controller {
    private readonly IPostLogic _postLogic;

    public PostController(IPostLogic postLogic) {
        _postLogic = postLogic;
    }

    public async Task<IActionResult> Index(int? id) {
        if (id == null)
        {
            return NotFound();
        }
        
        Post? post = await _postLogic.GetPostByIdAsync(id);
        if (post == null) {
            return NotFound();
        }
        List<Reply>? replies = await _postLogic.GetRepliesByPostIdAsync(id);
        var viewModel = new PostViewModel {
            Post = post,
            Replies = replies,
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

        if (await _postLogic.AddReplyAsync(reply) < 0) {
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
    public async Task<IActionResult> Create([Bind("Text")] Post post) {
        post.SubmitTime = DateTime.Now;
        int? userId = HttpContext.Session.GetInt32(SessionKeys.UserId);
        if (userId == null) {
            return View();
        }
        
        post.UserId = (int)userId;
        if (await _postLogic.AddPostAsync(post) < 0) {
            return View();
        }

        return RedirectToAction("Index", "BulletinBoard");
    }
}