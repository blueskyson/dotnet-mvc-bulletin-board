using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BulletinBoard.Models;

namespace BulletinBoard.Controllers;

public class BulletinBoardController : Controller {
    private readonly BulletinBoardDbContext _dbContext;

    public BulletinBoardController(BulletinBoardDbContext context) {
        _dbContext = context;
    }
    public async Task<IActionResult> Index() {
        return _dbContext.Users != null ? 
            View(await _dbContext.Users.ToListAsync()) :
            Problem("Entity set 'MvcMovieContext.Movie' is null.");
    }
}