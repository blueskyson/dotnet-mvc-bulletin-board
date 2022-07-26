using Microsoft.AspNetCore.Mvc;
using BulletinBoard.Models.BusinessLogic;
using BulletinBoard.Infrasructure;
using BulletinBoard.Utils;
using BulletinBoard.Models.Entities;

namespace BulletinBoard.Controllers;

[ServiceFilter(typeof(AuthorizationAttribute))]
public class UserController : Controller
{
    private readonly IUserLogic _userLogic;

    public UserController(IUserLogic userLogic)
    {
        _userLogic = userLogic;
    }

    public IActionResult ChangeDisplayName()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [TypeFilter(typeof(FormValidationAttribute), Arguments = new object[] { ViewDataKeys.DisplayName })]
    public async Task<IActionResult> ChangeDisplayName(string displayName)
    {
        int? userId = HttpContext.Session.GetInt32(SessionKeys.UserId);

        if (userId == null)
        {
            ViewData[ViewDataKeys.DisplayName] = "Session Error. Login again may fix the problem";
        }
        else if (await UpdateDisplayName(userId, displayName) == false)
        {
            ViewData[ViewDataKeys.DisplayName] = "Error changing name";
        }
        else
        {
            HttpContext.Session.SetString(SessionKeys.DisplayName, displayName);
            return RedirectToAction("Index", "BulletinBoard");
        }

        return View();
    }

    private async Task<bool> UpdateDisplayName(int? userId, string displayName)
    {
        User? currentUser = await _userLogic.GetUserByIdAsync((int)userId!);
        currentUser!.DisplayName = displayName;
        return await _userLogic.UpdateUserAsync(currentUser);
    }
}