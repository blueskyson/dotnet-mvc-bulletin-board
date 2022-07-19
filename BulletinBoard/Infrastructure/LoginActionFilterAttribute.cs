using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BulletinBoard.Utils.Validation;

namespace BulletinBoard.Infrasructure;

public class LoginActionFilterAttribute : Attribute, IActionFilter {
    private readonly IValidator _validator;
    
    public LoginActionFilterAttribute(IValidator validator) {
        _validator = validator;
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        Controller? controller = context.Controller as Controller;

        if (controller != null)
        {
            if (validateLoginForm(controller) == true)
                return;
            
            context.Result = new ViewResult {
                ViewName = "Index",
                ViewData = controller.ViewData,
                TempData = controller.TempData
            };
        }
    }

    private bool validateLoginForm(Controller controller)
    {
        if (!controller.ViewData.ModelState.IsValid)
            return false;

        var name = controller.HttpContext.Request.Form["Name"].ToString();
        if (!_validator.isValidName(name))
        {
            controller.ViewData["NameStatus"] = "Illegal character in name.";
            return false;
        }

        var password = controller.HttpContext.Request.Form["Password"].ToString();
        if (!_validator.isValidPassword(password))
        {
            controller.ViewData["PasswordStatus"] = "Illegal character in password.";
            return false;
        }

        return true;
    }
}