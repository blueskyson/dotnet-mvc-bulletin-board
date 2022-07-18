using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BulletinBoard.Models;
using BulletinBoard.Utils.Validation;

namespace BulletinBoard.Controllers;

public class LoginController : Controller {
    private readonly BulletinBoardDbContext _dbContext;
    private readonly IValidator _validator;

    public LoginController(BulletinBoardDbContext context, IValidator validator) {
        _dbContext = context;
        _validator = validator;
    }
    public IActionResult Index() {
        ViewData["NameStatus"] = "";
        ViewData["PasswordStatus"] = "";
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index([Bind("Name,Password")] User user)
    {
        ViewData["NameStatus"] = "";
        ViewData["PasswordStatus"] = "";

        if (!ModelState.IsValid)
            return View();
        else if (!_validator.isValidName(user.Name))
            ViewData["NameStatus"] = "Illegal character in name.";
        else if (!_validator.isValidPassword(user.Password))
            ViewData["PasswordStatus"] = "Illegal character in password.";
        else if (_dbContext.userExists(user) == null)
            ViewData["PasswordStatus"] = "Wrong name or password";
        else
            return RedirectToAction("Index", "BulletinBoard");
        return View();
    }
}