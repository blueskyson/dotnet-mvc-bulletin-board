using Microsoft.AspNetCore.Mvc;
using BulletinBoard.Models.BusinessLogic;
using BulletinBoard.Utils;
using BulletinBoard.Infrasructure;
using BulletinBoard.Models.Entities;

namespace BulletinBoard.Controllers;

/// <summary>
/// Deal with requests for login.
/// </summary>
public class LoginController : Controller
{
    private readonly ILoginLogic _loginLogic;

    /// <summary>
    /// Inject the business logic by DI Container.
    /// </summary>
    /// <param name="loginLogic">The business logic of LoginController.</param>
    public LoginController(ILoginLogic loginLogic)
    {
        _loginLogic = loginLogic;
    }

    /// <summary>
    /// Show Login page.
    /// </summary>
    /// <returns>The view of login page.</returns>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Receive login form and validate Name and Password of a User. 
    /// If login success, redirect to <see cref="BulletinBoardController.Index()"/>.
    /// </summary>
    /// <param name="user">A User entity containing Name and Password.</param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    [TypeFilter(typeof(FormValidationAttribute), Arguments = new object[] { ViewDataKeys.Name })]
    [TypeFilter(typeof(FormValidationAttribute), Arguments = new object[] { ViewDataKeys.Password })]
    public async Task<IActionResult> Index([Bind("Name,Password")] User user)
    {
        User? u = await _loginLogic.UserExists(user);

        if (u == null)
        {
            ViewData[ViewDataKeys.Password] = "Wrong name or password";
            return View();
        }

        UpdateSession(u);
        return RedirectToAction("Index", "BulletinBoard");
    }

    private void UpdateSession(User user)
    {
        HttpContext.Session.SetInt32(SessionKeys.UserId, user.Id!);
        HttpContext.Session.SetString(SessionKeys.DisplayName, user.DisplayName!);
    }
}