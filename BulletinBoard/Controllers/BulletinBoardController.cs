using Microsoft.AspNetCore.Mvc;
using BulletinBoard.Models;
using BulletinBoard.Infrasructure;
using BulletinBoard.Utils;

namespace BulletinBoard.Controllers;

[ServiceFilter(typeof(AuthorizationAttribute))]
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
}