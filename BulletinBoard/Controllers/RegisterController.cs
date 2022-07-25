using Microsoft.AspNetCore.Mvc;
using BulletinBoard.Utils;
using BulletinBoard.Models.Entities;
using BulletinBoard.Models.BusinessLogic;
using BulletinBoard.Infrasructure;

namespace BulletinBoard.Controllers;

public class RegisterController : Controller
{
    private readonly IRegisterLogic _registerLogic;

    public RegisterController(IRegisterLogic registerLogic)
    {
        _registerLogic = registerLogic;
    }

    public IActionResult Index()
    {
        return View();
    }

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
            return View();
        }

        if (await _registerLogic.AddUserAsync(user) < 0)
        {
            TempData["message"] = "Register Error, please try again!";
            return View();

        }

        TempData["message"] = "Register successfully!";
        return RedirectToAction("Index", "Login");
    }
}