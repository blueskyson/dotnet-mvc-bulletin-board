using Microsoft.AspNetCore.Mvc;
using BulletinBoard.Models;
using BulletinBoard.Utils.Validation;
using BulletinBoard.Infrasructure;

namespace BulletinBoard.Controllers;

public class LoginController : Controller
{
    private readonly IDbContext _dbContext;

    public LoginController(IDbContext context, IValidator validator)
    {
        _dbContext = context;
    }
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [TypeFilter(typeof(FormValidationAttribute), Arguments = new object[] {"Name"})]
    [TypeFilter(typeof(FormValidationAttribute), Arguments = new object[] {"Password"})]
    public IActionResult Index([Bind("Name,Password")] User user)
    {
        User? dbUser = _dbContext.UserExists(user);
        if (dbUser == null)
        {
            ViewData["Password"] = "Wrong name or password";
            return View();
        }

        HttpContext.Session.SetInt32("userid", dbUser.Id!);
        HttpContext.Session.SetString("displayname", dbUser.DisplayName!);
        return RedirectToAction("Index", "BulletinBoard");
    }
}