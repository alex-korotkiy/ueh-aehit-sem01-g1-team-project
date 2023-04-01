using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website.Infrastructure.ModelBinding;
using Website.Infrastructure.Repositories;
using Website.Models.Requests;

namespace Website.Controllers
{
    public class BooksController : Controller
    {
        IBooksRepository booksRepository;
        IUsersRepository usersRepository;

        public BooksController(IBooksRepository booksRepo, IUsersRepository usersRepo)
        {
            booksRepository = booksRepo;
            usersRepository = usersRepo;
        }

        [Route("[controller]/{id}")]
        public IActionResult Get(int id)
        {
            var book = booksRepository.Get(id);
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
