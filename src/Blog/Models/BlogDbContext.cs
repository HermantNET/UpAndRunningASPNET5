using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class BlogDbContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }

        public BlogDbContext()
        {
            Database.EnsureCreated();
        }

        public IQueryable<ArchivedPostSummary> GetArchivedPost()
        {
            return
                Posts
                    .GroupBy(x => new { x.Posted.Year, x.Posted.Month })
                    .Select(group => new ArchivedPostSummary
                    {
                        Count = group.Count(),
                        Year = group.Key.Year,
                        Month = group.Key.Month,
                    });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var connectionString = @"Server=(LocalDb)\MSSQLLocalDb;Database=Blog";
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ForSqlServerUseIdentityColumns();
        }
    }
}
