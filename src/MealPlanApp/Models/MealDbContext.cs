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
