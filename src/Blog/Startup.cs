using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Blog.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Blog
{
    public class Startup
    {
        //private readonly Platform _platform;

        public Startup(IHostingEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("config.json")
                //All environment variable in the process's context flow in as configuration values.
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            //_platform = new Platform();
        }

        public IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();

            services.AddMvc();
            services.AddScoped<Models.BlogDbContext>();
            services.AddScoped<Models.Identity.IdentityDataContext>();
            services.AddTransient<Models.FormattingService>();

            string IdentityConnectionString =
                "Server=(LocalDb)\\MSSQLLocalDb;Database=Blog_Identity";

            services.AddEntityFramework()
                .AddEntityFrameworkSqlServer()
                .AddDbContext<Models.Identity.IdentityDataContext>(dbConfig =>
                dbConfig.UseSqlServer(IdentityConnectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>();
        }


        //Development env
        public void ConfigureDevelopment(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(minLevel: LogLevel.Information);

            // StatusCode pages to gracefully handle status codes 400-599.
            app.UseStatusCodePagesWithRedirects("~/Home/StatusCodePage");

            // Display custom error page in production when error occurs
            // During development use the ErrorPage middleware to display error information in the browser
            app.UseDeveloperExceptionPage();

            app.UseDatabaseErrorPage();

            Configure(app);
        }


        //Staging env
        public void ConfigureStaging(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(minLevel: LogLevel.Warning);

            // StatusCode pages to gracefully handle status codes 400-599.
            app.UseStatusCodePagesWithRedirects("~/Home/StatusCodePage");

            app.UseExceptionHandler("/Home/Error");

            Configure(app);
        }

        //This method is invoked when ASPNETCORE_ENVIRONMENT is 'Production'
        //The allowed values are Development,Staging and Production
        public void ConfigureProduction(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(minLevel: LogLevel.Warning);

            // StatusCode pages to gracefully handle status codes 400-599.
            app.UseStatusCodePagesWithRedirects("~/Home/StatusCodePage");

            app.UseExceptionHandler("/Home/Error");

            Configure(app);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseIdentity();

            app.UseMvc(routes =>
                routes.MapRoute("Default",
                "{controller=Home}/{action=Index}/{id?}"));

            app.UseFileServer();
        } 
    }
}
