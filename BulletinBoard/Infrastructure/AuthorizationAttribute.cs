using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BulletinBoard.Utils;

namespace BulletinBoard.Infrasructure;

/// <summary>
/// Validate a session.
/// </summary>
public class AuthorizationAttribute : Attribute, IAuthorizationFilter
{
    /// <summary>
    /// An Overrided function for validating session.
    /// If a session timed out, redirect to <see cref="LoginController.Index()"/>.
    /// </summary>
    /// <param name="context"></param>
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        int? currentUserId = context.HttpContext.Session.GetInt32(SessionKeys.UserId);

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