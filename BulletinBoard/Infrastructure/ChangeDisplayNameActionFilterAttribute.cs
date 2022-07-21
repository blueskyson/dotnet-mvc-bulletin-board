using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BulletinBoard.Utils.Validation;

namespace BulletinBoard.Infrasructure;

public class ChangeDisplayNameActionFilterAttribute : Attribute, IActionFilter
{
    private readonly IValidator _validator;

    public ChangeDisplayNameActionFilterAttribute(IValidator validator)
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
            if (validateChangeDisplayNameForm(controller) == true)
                return;

            context.Result = new ViewResult
            {
                ViewName = "ChangeDisplayName",
                ViewData = controller.ViewData,
                TempData = controller.TempData
            };
        }
    }

    private bool validateChangeDisplayNameForm(Controller controller)
    {
        if (!controller.ViewData.ModelState.IsValid)
            return false;
        
        var displayName = controller.HttpContext.Request.Form["newDisplayName"].ToString();
        if (!_validator.IsValidDisplayName(displayName))
        {
            controller.ViewData["DisplayNameStatus"] = "Illegal character in display name.";
            return false;
        }

        return true;
    }
}