using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace BulletinBoard.Infrasructure;

public class AuthorizationFilterAttribute : ActionFilterAttribute, IAuthorizationFilter {
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        int? currentUserId = context.HttpContext.Session.GetInt32("userid");
        if (currentUserId == null)
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