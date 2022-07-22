using Microsoft.AspNetCore.Mvc;
using BulletinBoard.Models;
using BulletinBoard.Infrasructure;
using BulletinBoard.Models.Entities;

namespace BulletinBoard.Controllers;

public class RegisterController : Controller
{
    private readonly IDbContext _dbContext;

    public RegisterController(IDbContext context)
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
    [TypeFilter(typeof(FormValidationAttribute), Arguments = new object[] {"DisplayName"})]
    public IActionResult Index([Bind("Name,Password,DisplayName")] User user)
    {
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
}