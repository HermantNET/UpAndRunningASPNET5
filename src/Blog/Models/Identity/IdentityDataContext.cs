using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.Identity
{
    public class IdentityDataContext : IdentityDbContext<ApplicationUser>
    {

        public IdentityDataContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var connectionString = @"Server=(LocalDb)\MSSQLLocalDb;Database=Identity";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
