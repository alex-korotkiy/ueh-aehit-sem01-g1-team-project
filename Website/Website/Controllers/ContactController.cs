using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Controllers
{
    public class ContactController : BaseUIController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
