using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private BlogDbContext _db;

        public HomeController(BlogDbContext db)
        {
            _db = db;
        }

        // GET: /<controller>/
        public IActionResult Index(int page = 0)
        {
            var pageSize = 3;
            var skip = page * pageSize;

            var posts =
                _db.Posts
                .OrderByDescending(x => x.Posted)
                .Skip(skip)
                .Take(pageSize)
                .ToArray();

            var totalPosts = _db.Posts.Count();
            var totalPages = totalPosts / pageSize;
            var previousPage = page - 1;
            var nextPage = page + 1;

            ViewBag.PreviousPage = previousPage;
            ViewBag.HasPreviousPage = previousPage >= 0;
            ViewBag.NextPage = nextPage;
            ViewBag.HasNextPage = nextPage <= totalPages;

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView(posts); else return View(posts);
        }

        public IActionResult error()
        {
            return View();
        }

        public IActionResult StatusCodePage()
        {
            return View("~/Views/Shared/StatusCodePage.cshtml");
        }

    }

}
