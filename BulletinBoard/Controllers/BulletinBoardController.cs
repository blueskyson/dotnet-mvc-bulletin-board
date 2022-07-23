using Microsoft.AspNetCore.Mvc;
using BulletinBoard.Models;
using BulletinBoard.Infrasructure;
using BulletinBoard.Models.Entities;

namespace BulletinBoard.Controllers;

[ServiceFilter(typeof(AuthorizationAttribute))]
public class BulletinBoardController : Controller {
    private readonly IDbContext _dbContext;

    public BulletinBoardController(IDbContext context) {
        _dbContext = context;
    }
    public async Task<IActionResult> Index() {
        List<Post> viewModel = await _dbContext.GetAllPostsAsync();
        return View(viewModel);        
    }
}