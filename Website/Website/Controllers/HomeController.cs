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
using Website.Models.Converters;
using Website.Models.DbDto;
using Website.Models.Requests;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IBooksRepository booksRepository;
        IUsersRepository usersRepository;
        IRecommendationsRepository recommendationsRepository;

        public HomeController(ILogger<HomeController> logger, IBooksRepository booksRepo, IUsersRepository usersRepo, IRecommendationsRepository recoRepo)
        {
            _logger = logger;
            booksRepository = booksRepo;
            usersRepository = usersRepo;
            recommendationsRepository = recoRepo;
        }

        public IActionResult Index([FromCookie] Guid? UserId, BookUISearchRequest request)
        {
            var searchRequest = BookSearchConverter.Convert(request);

            long? lngUserId = null;

            if (UserId != null)
            {
                var user = usersRepository.GetByUniqueId(UserId.Value);
                if (user != null)
                {
                    lngUserId = user.Id;
                    searchRequest.UserId = user.Id;
                }
            }

            ViewData["Recommendations"] = recommendationsRepository.GetForUser(lngUserId);

            var list = booksRepository.Search(searchRequest);
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
