using Microsoft.AspNetCore.Mvc;
using BulletinBoard.Models;
using BulletinBoard.Models.Entities;
using BulletinBoard.Models.BusinessLogic;
using BulletinBoard.Infrasructure;
using BulletinBoard.Utils;

namespace BulletinBoard.Controllers;

/// <summary>
/// Deal with requests for viewing posts and its replies. 
/// It also deals with requests for creating new posts.
/// </summary>
[ServiceFilter(typeof(AuthorizationAttribute))]
public class PostController : Controller
{
    private readonly IPostLogic _postLogic;

    /// <summary>
    /// Inject the business logic by DI Container.
    /// </summary>
    /// <param name="postLogic">The business logic of PostController.</param>
    public PostController(IPostLogic postLogic)
    {
        _postLogic = postLogic;
    }

    /// <summary>
    /// Show the post of given id and its replies.
    /// If there's no such id, redirect to NotFound().
    /// </summary>
    /// <param name="id">The Id of a Post</param>
    /// <returns>The view of the post and replies.</returns>
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

    /// <summary>
    /// Receive reply form and save the reply to database.
    /// </summary>
    /// <param name="id">Id of the Post that a user replies to.</param>
    /// <param name="newReply">Text of the Reply.</param>
    /// <returns>The view of post and replies.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(int id, string newReply)
    {
        int? userId = HttpContext.Session.GetInt32(SessionKeys.UserId);

        if (userId != null)
            await AddReply(new Reply
            {
                PostId = id,
                UserId = (int)userId!,
                SubmitTime = DateTime.Now,
                Text = newReply,
            });
        else
            ViewData[ViewDataKeys.Reply] = "Error session. Log in again may fix the problem.";

        return RedirectToAction("Index", new {id});
    }

    private async Task AddReply(Reply reply)
    {
        if (await _postLogic.AddReplyAsync(reply) < 0)
            ViewData[ViewDataKeys.Reply] = "Error creating reply.";
    }

    /// <summary>
    /// Show create post page.
    /// </summary>
    /// <returns>The view for creating a post.</returns>
    public IActionResult Create()
    {
        return View();
    }

    /// <summary>
    /// Receive create-post form and validate Name and Password of a User.
    /// </summary>
    /// <param name="post">A Post entity containing Text.</param>
    /// <returns>If the post is saved to database successfully, redirect to <see cref="BulletinBoardController.Index()"/>.</returns>
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