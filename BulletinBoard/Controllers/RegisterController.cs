using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.Controllers;

public class RegisterController : Controller {
    public IActionResult Index() {
        return View();
    }
}