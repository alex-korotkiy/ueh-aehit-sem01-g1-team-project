using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Infrastructure.Filters
{
    public class UserIdFilter : IActionFilter
    {
        const string cookieName = "UserId";
        protected Guid UserId;
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var cookieOptions = new CookieOptions {
                SameSite = SameSiteMode.Strict,
                HttpOnly = true,
                Expires = DateTime.Now.AddYears(1),
                Path = "/"
            };

            context.HttpContext.Response.Cookies.Append(cookieName, UserId.ToString(), cookieOptions);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var userIdCookie = context.HttpContext.Request.Cookies[cookieName];
            if (!Guid.TryParse(userIdCookie, out UserId)) UserId = Guid.NewGuid();
        }
    }
}
