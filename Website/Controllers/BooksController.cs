using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website.Infrastructure.Repositories;
using Website.Models.Requests;

namespace Website.Controllers
{
    public class BooksController : Controller
    {
        IBooksRepository repository;

        public BooksController(IBooksRepository rep)
        {
            repository = rep;
        }

        [Route("[controller]/{id}")]
        public IActionResult Get(int id)
        {
            var book = repository.Get(id);
            return Json(book);
        }

        public IActionResult Search(BookSearchRequest request) 
        {
            var list = repository.Search(request);
            return Json(list);
        }
    }
}
