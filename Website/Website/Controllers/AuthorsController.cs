using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website.Infrastructure.ModelBinding;
using Website.Infrastructure.Repositories;

namespace Website.Controllers
{
    public class AuthorsController : BaseUIController
    {
        IAuthorsRepository authorsRepository;
        IUsersRepository usersRepository;
        IRecommendationsRepository recommendationsRepository;
        public AuthorsController(ILogger<AuthorsController> logger, IAuthorsRepository authorsRepo, IUsersRepository usersRepo, IRecommendationsRepository recommendationsRepo) : base(logger)
        {
            authorsRepository = authorsRepo;
            usersRepository = usersRepo;
            recommendationsRepository = recommendationsRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("[controller]")]
        [Route("[controller]/{id}")]
        public IActionResult Index([FromCookie] Guid? UserId, string id = "")
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

            var authors = authorsRepository.GetAuthors(id);
            return Json(authors);
        }
    }
}
