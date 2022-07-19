using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace BulletinBoard.Infrasructure;

public class AuthorizationFilterAttribute : ActionFilterAttribute, IAuthorizationFilter {
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        string? currentUser = Convert.ToString(context.HttpContext.Session.GetString("username"));
        if (string.IsNullOrEmpty(currentUser))
        {
            context.Result = new RedirectToRouteResult(
                new RouteValueDictionary { 
                    {"controller", "Login"}, 
                    {"action", "Index"} 
                }
            );
        }
    }
}