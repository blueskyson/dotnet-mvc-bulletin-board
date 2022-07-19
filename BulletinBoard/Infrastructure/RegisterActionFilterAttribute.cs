using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BulletinBoard.Utils.Validation;

namespace BulletinBoard.Infrasructure;

public class RegisterActionFilterAttribute : Attribute, IActionFilter
{
    private readonly IValidator _validator;

    public RegisterActionFilterAttribute(IValidator validator)
    {
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
            if (validateRegisterForm(controller) == true)
                return;

            context.Result = new ViewResult
            {
                ViewName = "Index",
                ViewData = controller.ViewData,
                TempData = controller.TempData
            };
        }
    }

    private bool validateRegisterForm(Controller controller)
    {
        if (!controller.ViewData.ModelState.IsValid)
            return false;

        var name = controller.HttpContext.Request.Form["Name"].ToString();
        if (!_validator.isValidName(name))
        {
            controller.ViewData["NameStatus"] = "Illegal character in name.";
            return false;
        }

        var displayName = controller.HttpContext.Request.Form["DisplayName"].ToString();
        if (!_validator.isValidDisplayName(displayName))
        {
            controller.ViewData["DisplayNameStatus"] = "Illegal character in name.";
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