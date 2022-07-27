using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BulletinBoard.Utils.Validation;

namespace BulletinBoard.Infrasructure;

/// <summary>
/// Validate a input value of a form.
/// </summary>
public class FormValidationAttribute : Attribute, IActionFilter
{
    private readonly IValidator _validator;
    private readonly string _inputName;

    /// <summary>
    /// Inject the validator by DI Container and specify a input name.
    /// It automatically fetches the value of a given input name and validate it.
    /// </summary>
    /// <param name="validator">An injected Validator</param>
    /// <param name="inputName">An input name of a value to validate.</param>
    public FormValidationAttribute(IValidator validator, string inputName)
    {
        _validator = validator;
        _inputName = inputName;
    }

    /// <summary>
    /// Overrided function. Currently no effect.
    /// </summary>
    /// <param name="context"></param>
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    /// <summary>
    /// An overrided function to validate the input value.
    /// </summary>
    /// <param name="context"></param>
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