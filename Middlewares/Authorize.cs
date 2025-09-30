using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using P1WebMVC.Data;
using P1WebMVC.Interfaces;

namespace P1WebMVC.Middlewares;

[AttributeUsage(AttributeTargets.All)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{


    public void OnAuthorization(AuthorizationFilterContext context)
    {

        var token = context.HttpContext.Request.Cookies["authToken"];

        if (token == null)
        {
            context.Result = new RedirectToActionResult("Login", "User", null);
            return;
        }

        var tokenService = context.HttpContext.RequestServices.GetService(typeof(ITokenService)) as ITokenService;

        var userId = tokenService?.VerifyTokenAndGetId(token);

        if (userId == Guid.Empty)
        {
            context.Result = new RedirectToActionResult("Login", "User", null);
            return;

        }

        var dbContext = context.HttpContext.RequestServices.GetService(typeof(SqlDbContext)) as SqlDbContext;

        var user = dbContext?.Users.Find(userId);


        // sesssion storage
        context.HttpContext.Items["userId"] = user?.UserId;

        context.HttpContext.Items["user"] = user;




    }
}
