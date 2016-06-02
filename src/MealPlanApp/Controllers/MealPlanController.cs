using MealPlanApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanApp.Controllers
{
    public class MealPlanController : Controller
    {
        readonly MealDbContext _dataContext;

        public MealPlanController(MealDbContext dataContext)
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
        public async Task<IActionResult> Create(MealPlan mealPlan)
        {
            if (!ModelState.IsValid)
                return View(mealPlan);

            mealPlan.Author = User.Identity.Name;

            //Make sure ratios are within 100%
            mealPlan.MacroRatio = MealPlanCalculator.ratioCheck(mealPlan.MacroRatio);

            //Determine whether user already has a meal plan
            if (_dataContext.MealPlans.Any(x => x.Author == User.Identity.Name))
            {
                //if users already has meal plan, update values
                _dataContext.MealPlans
                   .Where(x => x.Author == User.Identity.Name).Single().TotalCalories = mealPlan.TotalCalories;
                _dataContext.MealPlans
                   .Where(x => x.Author == User.Identity.Name).Single().Carbohydrates = mealPlan.Carbohydrates;
                _dataContext.MealPlans
                   .Where(x => x.Author == User.Identity.Name).Single().Proteins = mealPlan.Proteins;
                _dataContext.MealPlans
                   .Where(x => x.Author == User.Identity.Name).Single().Fats = mealPlan.Fats;
            } else
            {
            _dataContext.MealPlans.Add(mealPlan);
            }

            //Commit changes to database
            await _dataContext.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Generate(MealPlan mealPlan)
        {
            mealPlan = _dataContext.MealPlans
                   .Where(x => x.Author == User.Identity.Name).Single();

            mealPlan.Plan = MealPlanCalculator.MyMealPlan(mealPlan.TotalCalories, mealPlan.MacroRatio, User.Identity.Name);

            if (_dataContext.MealPlans.Any(x => x.Author == User.Identity.Name))
            {
                _dataContext.MealPlans
                   .Where(x => x.Author == User.Identity.Name).Single().Plan = mealPlan.Plan;
            }

            await _dataContext.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
