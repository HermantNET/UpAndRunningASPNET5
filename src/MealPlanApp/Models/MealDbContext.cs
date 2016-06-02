using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealPlanApp.Models
{
    public class MealDbContext : DbContext
    {
        public DbSet<Food> Foods { get; set; }
        public DbSet<MealPlan> MealPlans { get; set; }

        public MealDbContext()
        {
            Database.EnsureCreated();
        }

        //public IQueryable<FoodsSummary> GetFoods()
        //{
        //    return
        //        Foods
        //            .GroupBy(x => new { x.Name, x.Calories, x.Breakfast, x.Lunch, x.Dinner, x.Snack })
        //            .Select(group => new FoodsSummary
        //            {
        //                Name = group.Key.Name,
        //                Calories = group.Key.Calories,
        //                Breakfast = group.Key.Breakfast,
        //                Lunch = group.Key.Lunch,
        //                Dinner = group.Key.Dinner,
        //                Snack = group.Key.Snack
        //            });
        //}

        public IQueryable<Food> GetFoods(string UserIdentity)
        {
            return Foods.Where(x => x.Author == UserIdentity);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var connectionString = @"Server=(LocalDb)\MSSQLLocalDb;Database=Meal";
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ForSqlServerUseIdentityColumns();
        }
    }
}
