using Microsoft.AspNetCore.Mvc;
using BulletinBoard.Utils.Validation;
using BulletinBoard.Models;
using BulletinBoard.Infrasructure;

namespace BulletinBoard.Controllers;

public class RegisterController : Controller
{

    private readonly IDbContext _dbContext;

    public RegisterController(IDbContext context, IValidator validator)
    {
        _dbContext = context;
    }

    public IActionResult Index()
    {
        InitializeViewData();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [TypeFilter(typeof(FormValidationAttribute), Arguments = new object[] {"Name"})]
    [TypeFilter(typeof(FormValidationAttribute), Arguments = new object[] {"Password"})]
    [TypeFilter(typeof(FormValidationAttribute), Arguments = new object[] {"DisplayName"})]
    public IActionResult Index([Bind("Name,Password,DisplayName")] User user)
    {
        InitializeViewData();

        if (_dbContext.UserNameExists(user.Name!))
        {
            ViewData["Name"] = "User name exists.";
            return View();
        }

        if (_dbContext.CreateUser(user))
        {
            TempData["message"] = "Register successfully!";
            return RedirectToAction("Index", "Login");
        }

        TempData["message"] = "Register Error, please try again!";
        return View();
    }

    private void InitializeViewData()
    {
        ViewData["NameStatus"] = "";
        ViewData["PasswordStatus"] = "";
        ViewData["DisplayNameStatus"] = "";
    }
}