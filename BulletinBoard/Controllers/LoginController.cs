using Microsoft.AspNetCore.Mvc;
using BulletinBoard.Models.BusinessLogic;
using BulletinBoard.Utils;
using BulletinBoard.Infrasructure;
using BulletinBoard.Models.Entities;

namespace BulletinBoard.Controllers;

public class LoginController : Controller
{
    private readonly ILoginLogic _loginLogic;

    public LoginController(ILoginLogic loginLogic)
    {
        _loginLogic = loginLogic;
    }

    public IActionResult Index()
    {
        return View();
    }

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

    private void UpdateSession(User user) {
        HttpContext.Session.SetInt32(SessionKeys.UserId, user.Id!);
        HttpContext.Session.SetString(SessionKeys.DisplayName, user.DisplayName!);
    }
}