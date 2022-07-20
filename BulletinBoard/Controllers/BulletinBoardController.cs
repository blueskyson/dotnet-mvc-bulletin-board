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

    public async Task<IActionResult> ChangeDisplayName() {
        return View();        
    }

    public async Task<IActionResult> CreatePost() {
        return View();        
    }
}