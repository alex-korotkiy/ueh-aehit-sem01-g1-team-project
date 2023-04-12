using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Controllers
{
    public class ContactController : BaseUIController
    {
        public ContactController(ILogger<ContactController> logger) : base(logger)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
