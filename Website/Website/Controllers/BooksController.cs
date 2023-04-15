using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website.Infrastructure.ModelBinding;
using Website.Infrastructure.Repositories;
using Website.Models.Requests;

namespace Website.Controllers
{
    public class BooksController : BaseApiController
    {
        IBooksRepository booksRepository;
        IUsersRepository usersRepository;

        public BooksController(ILogger<BooksController> logger, IBooksRepository booksRepo, IUsersRepository usersRepo) : base(logger)
        {
            booksRepository = booksRepo;
            usersRepository = usersRepo;
        }

        [Route("[controller]/{id}")]
        public IActionResult Get([FromCookie] Guid? UserId, int id)
        {
            long? lngUserId = null;

            if (UserId != null)
            {
                var user = usersRepository.GetByUniqueId(UserId.Value);
                if (user != null) lngUserId = user.Id;
            }

            var book = booksRepository.Get(id, lngUserId);
            return Json(book);
        }

        public IActionResult Search([FromCookie] Guid? UserId, BookSearchRequest request)
        {
            if (UserId != null & request.RatedOnly != 0)
            {
                var user = usersRepository.GetByUniqueId(UserId.Value);
                if (user != null) request.UserId = user.Id;
            }

            var list = booksRepository.Search(request);
            return Json(list);
        }
    }
}
