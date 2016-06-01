using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Controllers
{
    public class PostsController : Controller
    {
        readonly BlogDbContext _dataContext;

        public PostsController(BlogDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post)
        {
            if (!ModelState.IsValid)
                return View(post);

            post.Posted = DateTime.Now;
            post.Author = User.Identity.Name;

            _dataContext.Posts.Add(post);
            await _dataContext.SaveChangesAsync();

            return RedirectToAction("Post", new { post.Posted.Year, post.Posted.Month, post.Key });
        }

        public IActionResult Post(long id)
        {
            var post = _dataContext.Posts.Single(x => x.Id == id);

            return View(post);
        }

        [Route("Posts/{year:int}/{month:int}/{key}")]
        public IActionResult Post(int year, int month, string key)
        {
            var post = _dataContext.Posts.SingleOrDefault(
                x => x.Posted.Year == year && x.Posted.Month == month &&
                    x.Key == key.ToLower());

            return View(post);
        }

        public IActionResult CauseAnError()
        {
            throw new Exception("Error!");
        }
    }
}
