using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Controllers
{
    public class DoErrorController : BaseUIController
    {
        public DoErrorController(ILogger<DoErrorController> logger) : base(logger)
        {
        }

        public IActionResult Index()
        {
            throw new Exception("Test exception");
        }
    }
}
