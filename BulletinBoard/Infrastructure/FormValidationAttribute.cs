using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BulletinBoard.Utils.Validation;

namespace BulletinBoard.Infrasructure;

public class FormValidationAttribute : Attribute, IActionFilter
{
    private readonly IValidator _validator;
    private readonly string _inputName;

    public FormValidationAttribute(IValidator validator, string inputName)
    {
        _validator = validator;
        _inputName = inputName;
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        Controller? controller = context.Controller as Controller;

        if (controller != null)
        {
            if (ValidateInputValue(controller) == true)
                return;
            context.Result = new ViewResult { ViewData = controller.ViewData };
        }
    }

    private bool ValidateInputValue(Controller controller)
    {
        var value = controller.HttpContext.Request.Form[_inputName].ToString();

        if (controller.ViewData.ModelState.IsValid &&
            _validator.IsValidString(value))
            return true;

        controller.ViewData[_inputName] = "Illegal character in " + _inputName + ".";
        return false;
    }
}