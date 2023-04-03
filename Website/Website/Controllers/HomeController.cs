using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Website.Infrastructure.ModelBinding;
using Website.Infrastructure.Repositories;
using Website.Models;
using Website.Models.Requests;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IBooksRepository booksRepository;
        IUsersRepository usersRepository;

        public HomeController(ILogger<HomeController> logger, IBooksRepository booksRepo, IUsersRepository usersRepo)
        {
            _logger = logger;
            booksRepository = booksRepo;
            usersRepository = usersRepo;
        }

        public IActionResult Index([FromCookie] Guid? UserId, BookSearchRequest request)
        {
            if (UserId != null & request.RatedOnly != 0)
            {
                var user = usersRepository.GetByUniqueId(UserId.Value);
                if (user != null) request.UserId = user.Id;
            }

            var list = booksRepository.Search(request);

            return View(list);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
