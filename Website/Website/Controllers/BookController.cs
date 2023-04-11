using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website.Infrastructure.ModelBinding;
using Website.Infrastructure.Repositories;

namespace Website.Controllers
{
    public class BookController : BaseUIController
    {
        IBooksRepository booksRepository;
        IUsersRepository usersRepository;
        IRecommendationsRepository recommendationsRepository;

        public BookController(IBooksRepository booksRepo, IUsersRepository usersRepo, IRecommendationsRepository recoRepo)
        {
            booksRepository = booksRepo;
            usersRepository = usersRepo;
            recommendationsRepository = recoRepo;
        }

        [Route("[controller]/{id}")]
        public IActionResult Index([FromCookie] Guid? UserId, int id)
        {
            long? lngUserId = null;

            if (UserId != null)
            {
                var user = usersRepository.GetByUniqueId(UserId.Value);
                if (user != null)
                {
                    lngUserId = user.Id;
                }
            }

            ViewData["Recommendations"] = recommendationsRepository.GetForUser(lngUserId);

            var book = booksRepository.Get(id);
            return View(book);
        }
    }
}
