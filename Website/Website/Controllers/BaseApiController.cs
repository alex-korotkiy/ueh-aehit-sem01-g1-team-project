using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Controllers
{
    public class BaseApiController : BaseController
    {
        public BaseApiController(ILogger<BaseApiController> logger) : base(logger)
        {
        }

        [NonAction]
        public IActionResult ShowModelErrors(ModelStateDictionary modelState)
        {

            var result = (from v in modelState.Values
                          from e in v.Errors
                          select e.ErrorMessage).ToList();

            return Json(result);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            
            if (!ModelState.IsValid)
            {
                Response.StatusCode = 422;
                context.Result = ShowModelErrors(ModelState);
            }
        }
    }
}
