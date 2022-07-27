using Microsoft.AspNetCore.Mvc;
using BulletinBoard.Utils;
using BulletinBoard.Models.Entities;
using BulletinBoard.Models.BusinessLogic;
using BulletinBoard.Infrasructure;

namespace BulletinBoard.Controllers;

/// <summary>
/// Deal with requests for registeration.
/// </summary>
public class RegisterController : Controller
{
    private readonly IRegisterLogic _registerLogic;

    /// <summary>
    /// Inject the business logic by DI Container.
    /// </summary>
    /// <param name="registerLogic">The business logic of RegisterController.</param>
    public RegisterController(IRegisterLogic registerLogic)
    {
        _registerLogic = registerLogic;
    }

    /// <summary>
    /// Show registeration page.
    /// </summary>
    /// <returns>The view of registeration page.</returns>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Receive login form and validate Name, DisplayName and Password of a new User.
    /// </summary>
    /// <param name="user">A User entity containing Name, DisplayName and Password.</param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    [TypeFilter(typeof(FormValidationAttribute), Arguments = new object[] { ViewDataKeys.Name })]
    [TypeFilter(typeof(FormValidationAttribute), Arguments = new object[] { ViewDataKeys.Password })]
    [TypeFilter(typeof(FormValidationAttribute), Arguments = new object[] { ViewDataKeys.DisplayName })]
    public async Task<IActionResult> Index([Bind("Name,Password,DisplayName")] User user)
    {
        if (_registerLogic.UserNameExists(user.Name!))
        {
            ViewData[ViewDataKeys.Name] = "User name exists.";
        }
        else if (await _registerLogic.AddUserAsync(user) < 0)
        {
            TempData[TempDataKeys.Message] = "Register Error, please try again!";
        }
        else
        {
            TempData[TempDataKeys.Message] = "Register successfully!";
            return RedirectToAction("Index", "Login");
        }

        return View();
    }
}