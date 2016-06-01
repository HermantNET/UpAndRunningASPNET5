using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.ViewComponents
{
    [ViewComponent]
    public class ArchivedPostViewComponent : ViewComponent
    {
        readonly BlogDbContext _db;

        public ArchivedPostViewComponent(Blog.Models.BlogDbContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke()
        {
            var archivedPost = _db.GetArchivedPost().ToArray();
            return View(archivedPost);
        }
    }
}
