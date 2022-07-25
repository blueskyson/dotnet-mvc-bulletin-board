using Microsoft.AspNetCore.Mvc;
using BulletinBoard.Models;
using BulletinBoard.Models.Entities;
using BulletinBoard.Models.BusinessLogic;
using BulletinBoard.Infrasructure;
using BulletinBoard.Utils;

namespace BulletinBoard.Controllers;

[ServiceFilter(typeof(AuthorizationAttribute))]
public class PostController : Controller
{
    private readonly IPostLogic _postLogic;

    public PostController(IPostLogic postLogic)
    {
        _postLogic = postLogic;
    }

    public async Task<IActionResult> Index(int? id)
    {
        Post? post = await GetPost(id);

        if (post == null)
            return NotFound();

        return View(await GeneratePostViewModel(post));
    }

    private async Task<Post?> GetPost(int? id)
    {
        if (id == null)
            return null;
        return await _postLogic.GetPostByIdAsync(id);
    }

    private async Task<PostViewModel> GeneratePostViewModel(Post? post)
    {
        List<Reply>? replies = await _postLogic.GetRepliesByPostIdAsync(post!.Id);
        return new PostViewModel
        {
            Post = post,
            Replies = replies,
        };
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(int id, String NewReply)
    {
        int? userId = HttpContext.Session.GetInt32(SessionKeys.UserId);

        if (userId != null)
            await AddReply(new Reply
            {
                PostId = id,
                UserId = (int)userId!,
                SubmitTime = DateTime.Now,
                Text = NewReply,
            });
        else
            ViewData[ViewDataKeys.Reply] = "Error session. Log in again may fix the problem.";

        return await Index(id);
    }

    private async Task AddReply(Reply reply)
    {
        if (await _postLogic.AddReplyAsync(reply) < 0)
            ViewData[ViewDataKeys.Reply] = "Error creating reply.";
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Text")] Post post)
    {
        int? userId = HttpContext.Session.GetInt32(SessionKeys.UserId);

        if (userId != null)
        {
            post.SubmitTime = DateTime.Now;
            post.UserId = (int)userId;
            if (await AddPost(post))
                return RedirectToAction("Index", "BulletinBoard");
        }
        else
            ViewData[ViewDataKeys.Post] = "Error session. Log in again may fix the problem.";

        return View();
    }

    private async Task<bool> AddPost(Post post)
    {
        if (await _postLogic.AddPostAsync(post) < 0)
        {
            ViewData[ViewDataKeys.Post] = "Error creating post.";
            return false;
        }

        return true;
    }
}