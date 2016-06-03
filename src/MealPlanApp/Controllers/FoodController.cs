using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MealPlanApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MealPlanApp.Controllers
{
    public class FoodController : Controller
    {
        readonly MealDbContext _dataContext;

        public FoodController(MealDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            var Foods = _dataContext.Foods.Where(x => x.Author == User.Identity.Name).ToArray();

            return View(Foods);
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
        public async Task<IActionResult> Create(Food food)
        {
            if (!ModelState.IsValid)
                return View(food);

            food.Author = User.Identity.Name;

            _dataContext.Foods.Add(food);
            await _dataContext.SaveChangesAsync();

            return RedirectToAction("Food", food);
        }

        public IActionResult Food(long FoodID)
        {
            var food = _dataContext.Foods.Where(x => x.FoodID == FoodID);

            return View(food);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Delete(long FoodID)
        {
            var foo = _dataContext.Foods.Single(x => x.FoodID == FoodID);
            _dataContext.Foods.Remove(foo);
            _dataContext.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
