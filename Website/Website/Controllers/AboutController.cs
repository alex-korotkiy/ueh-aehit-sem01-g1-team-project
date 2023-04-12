using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Controllers
{
    public class AboutController : BaseUIController
    {
        public AboutController(ILogger<AboutController> logger) : base(logger)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
