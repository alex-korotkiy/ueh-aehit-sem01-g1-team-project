using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Controllers
{
    public class BaseApiController : Controller
    {
        public IActionResult ShowModelErrors(ModelStateDictionary modelState)
        {

            var result = (from v in modelState.Values
                          from e in v.Errors
                          select e.ErrorMessage).ToList();

            return Json(result);
        }
    }
}
