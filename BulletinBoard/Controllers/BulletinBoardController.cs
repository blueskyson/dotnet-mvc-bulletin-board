using Microsoft.AspNetCore.Mvc;
using BulletinBoard.Models.BusinessLogic;
using BulletinBoard.Infrasructure;
using BulletinBoard.Models.Entities;

namespace BulletinBoard.Controllers;

/// <summary>
/// Deal with requests for viewing posts.
/// This can be seen as the controller of the main page.
/// </summary>
[ServiceFilter(typeof(AuthorizationAttribute))]
public class BulletinBoardController : Controller
{
    private readonly IBulletinBoardLogic _bulletinBoardLogic;

    /// <summary>
    /// Inject the business logic by DI Container. 
    /// </summary>
    /// <param name="bulletinBoardLogic">The business logic of BulletinBoardController.</param>
    public BulletinBoardController(IBulletinBoardLogic bulletinBoardLogic)
    {
        _bulletinBoardLogic = bulletinBoardLogic;
    }

    /// <summary>
    /// Show all posts on the BulletinBoard page, which can be seen as the main page of this application.
    /// </summary>
    /// <returns>The view of BulletinBoard.</returns>
    public async Task<IActionResult> Index()
    {
        List<Post> viewModel = await _bulletinBoardLogic.GetAllPostsAsync();
        return View(viewModel);
    }
}