namespace Backend.Authorization;

using Backend.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeUserRoleAttribute: Attribute , IAuthorizationFilter
{
    private readonly string _roleName = UserRole.USERROLE;
    private readonly string _roleValue;
    public AuthorizeUserRoleAttribute(string roleValue)
    {
        _roleValue = roleValue;
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // authorization
        User user = (User)context.HttpContext.Items["User"];
        if(user.UserRole == null || !user.UserRole.Equals(_roleValue))
        {
            context.Result = new JsonResult(new { message = "Access Forbidden" }) { StatusCode = StatusCodes.Status403Forbidden };
        }
    }
}