using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Controllers
{
    public class DoErrorController : Controller
    {
        public IActionResult Index()
        {
            throw new Exception("Test exception");
        }
    }
}
