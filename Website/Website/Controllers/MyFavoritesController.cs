using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website.Infrastructure.ModelBinding;
using Website.Infrastructure.Repositories;
using Website.Models.Converters;
using Website.Models.Requests;
using Website.Models.Web;

namespace Website.Controllers
{
    public class MyFavoritesController : BaseUIController
    {
        IBooksRepository booksRepository;
        IUsersRepository usersRepository;
        IRecommendationsRepository recommendationsRepository;

        public MyFavoritesController(ILogger<HomeController> logger, IBooksRepository booksRepo, IUsersRepository usersRepo, IRecommendationsRepository recoRepo) : base(logger)
        {
            booksRepository = booksRepo;
            usersRepository = usersRepo;
            recommendationsRepository = recoRepo;
        }

        public IActionResult Index([FromCookie] Guid? UserId, BookUISearchRequest request)
        {
            request.RatedOnly = 1;

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
            return View(new BooksSearchAndResult() { Request = request, Result = list });

        }
    }
}
