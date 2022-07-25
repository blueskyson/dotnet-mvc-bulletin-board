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
    [TypeFilter(typeof(FormValidationAttribute), Arguments = new object[] {"Name"})]
    [TypeFilter(typeof(FormValidationAttribute), Arguments = new object[] {"Password"})]
    public async Task<IActionResult> Index([Bind("Name,Password")] User user)
    {
        User? u = await _loginLogic.UserExists(user);
        if (u == null)
        {
            ViewData["Password"] = "Wrong name or password";
            return View();
        }

        HttpContext.Session.SetInt32(SessionKeys.UserId, u.Id!);
        HttpContext.Session.SetString(SessionKeys.DisplayName, u.DisplayName!);
        return RedirectToAction("Index", "BulletinBoard");
    }
}