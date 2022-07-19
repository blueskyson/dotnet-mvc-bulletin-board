using Microsoft.AspNetCore.Mvc;
using BulletinBoard.Utils.Validation;
using BulletinBoard.Models;
using BulletinBoard.Infrasructure;

namespace BulletinBoard.Controllers;

public class RegisterController : Controller {

    private readonly IDbContext _dbContext;
    private readonly IValidator _validator;

    public RegisterController(IDbContext context, IValidator validator) {
        _dbContext = context;
        _validator = validator;
    }

    public IActionResult Index() {
        InitializeViewData();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ServiceFilter(typeof(RegisterActionFilterAttribute))]
    public IActionResult Index([Bind("Name,Password,DisplayName")] User user)
    {
        InitializeViewData();

        if (_dbContext.userNameExists(user.Name!))
        {
            ViewData["NameStatus"] = "User name exists.";
            return View();
        }

        TempData["message"] = "Register successfully!";
        return RedirectToAction("Index", "Login");
    }

    private void InitializeViewData() {
        ViewData["NameStatus"] = "";
        ViewData["PasswordStatus"] = "";
        ViewData["DisplayNameStatus"] = "";
    }
}