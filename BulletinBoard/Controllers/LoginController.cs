using Microsoft.AspNetCore.Mvc;
using BulletinBoard.Models;
using BulletinBoard.Utils.Validation;
using BulletinBoard.Infrasructure;

namespace BulletinBoard.Controllers;

public class LoginController : Controller
{
    private readonly BulletinBoardDbContext _dbContext;
    private readonly IValidator _validator;

    public LoginController(BulletinBoardDbContext context, IValidator validator)
    {
        _dbContext = context;
        _validator = validator;
    }
    public IActionResult Index()
    {
        InitializeViewData();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ServiceFilter(typeof(LoginActionFilterAttribute))]
    public IActionResult Index([Bind("Name,Password")] User user)
    {
        InitializeViewData();

        if (_dbContext.userExists(user) == null)
        {
            ViewData["PasswordStatus"] = "Wrong name or password";
            return View();
        }

        HttpContext.Session.SetString("username", user.Name!);
        return RedirectToAction("Index", "BulletinBoard");
    }

    private void InitializeViewData()
    {
        ViewData["NameStatus"] = "";
        ViewData["PasswordStatus"] = "";
    }
}