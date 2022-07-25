using Microsoft.AspNetCore.Mvc;
using BulletinBoard.Models.BusinessLogic;
using BulletinBoard.Infrasructure;
using BulletinBoard.Models.Entities;

namespace BulletinBoard.Controllers;

[ServiceFilter(typeof(AuthorizationAttribute))]
public class BulletinBoardController : Controller
{
    private readonly IBulletinBoardLogic _bulletinBoardLogic;

    public BulletinBoardController(IBulletinBoardLogic bulletinBoardLogic)
    {
        _bulletinBoardLogic = bulletinBoardLogic;
    }
    public async Task<IActionResult> Index()
    {
        List<Post> viewModel = await _bulletinBoardLogic.GetAllPostsAsync();
        return View(viewModel);
    }
}