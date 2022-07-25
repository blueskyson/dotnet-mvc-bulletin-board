using Microsoft.AspNetCore.Mvc;
using BulletinBoard.Models;
using BulletinBoard.Models.BusinessLogic;
using BulletinBoard.Infrasructure;
using BulletinBoard.Utils;
using BulletinBoard.Models.Entities;

namespace BulletinBoard.Controllers;

[ServiceFilter(typeof(AuthorizationAttribute))]
public class UserController : Controller {
    private readonly IUserLogic _userLogic;

    public UserController(IUserLogic userLogic) {
        _userLogic = userLogic;
    }

    public IActionResult ChangeDisplayName() {
        return View();        
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [TypeFilter(typeof(FormValidationAttribute), Arguments = new object[] { ViewDataKeys.DisplayName })]
    public async Task<IActionResult> ChangeDisplayName(String DisplayName) {
        int? userId = HttpContext.Session.GetInt32(SessionKeys.UserId);
        if (userId == null) {
            ViewData[ViewDataKeys.DisplayName] = "No such user. Log in again may fix the problem";
            return View();
        }

        User? currentUser = await _userLogic.GetUserByIdAsync((int)userId!);
        currentUser!.DisplayName = DisplayName;
        if (await _userLogic.UpdateUserAsync(currentUser) == false) {
            ViewData[ViewDataKeys.DisplayName] = "Error changing name";
            return View();
        }

        HttpContext.Session.SetString(SessionKeys.DisplayName, DisplayName);
        return RedirectToAction("Index", "BulletinBoard");
    }
}