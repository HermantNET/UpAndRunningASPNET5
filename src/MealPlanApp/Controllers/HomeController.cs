using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MealPlanApp.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MealPlanApp.Controllers
{
    public class HomeController : Controller
    {
        private MealDbContext _db;

        public HomeController(MealDbContext db)
        {
            _db = db;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var mealPlan =
                _db.MealPlans
                .Single(x => x.Author == User.Identity.Name);

            return View(mealPlan);
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
