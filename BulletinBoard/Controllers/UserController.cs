using Microsoft.AspNetCore.Mvc;
using BulletinBoard.Models.BusinessLogic;
using BulletinBoard.Infrasructure;
using BulletinBoard.Utils;
using BulletinBoard.Models.Entities;

namespace BulletinBoard.Controllers;

/// <summary>
/// Deal with requests for changing user's information.
/// </summary>
[ServiceFilter(typeof(AuthorizationAttribute))]
public class UserController : Controller
{
    private readonly IUserLogic _userLogic;

    /// <summary>
    /// Inject the business logic by DI Container.
    /// </summary>
    /// <param name="userLogic">The business logic of UserController.</param>
    public UserController(IUserLogic userLogic)
    {
        _userLogic = userLogic;
    }

    /// <summary>
    /// Show changing DisplayName page.
    /// </summary>
    /// <returns>The view of changing-DisplayName page</returns>
    public IActionResult ChangeDisplayName()
    {
        return View();
    }

    /// <summary>
    /// Receive changing-DisplayName form and validate display name.
    /// </summary>
    /// <param name="displayName">A new display name requested by user.</param>
    /// <returns>If the DisplayName is updated successfully, redirect to <see cref="BulletinBoardController.Index()"/>.</returns>
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