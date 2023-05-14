using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website.Infrastructure.Filters;
using Website.Infrastructure.Middleware;
using Website.Infrastructure.ModelBinding;
using Website.Infrastructure.Repositories;

namespace Website
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("siteDb");

            services.AddTransient<IBooksRepository, BooksRepository>(provider => new BooksRepository(connectionString));
            services.AddTransient<IUsersRepository, UsersRepository>(provider => new UsersRepository(connectionString));
            services.AddTransient<IRecommendationsRepository, RecommendationsRepository>(provider => new RecommendationsRepository(connectionString));
            services.AddTransient<IRatingsRepository, RatingsRepository>(provider => new RatingsRepository(connectionString));
            services.AddTransient<IAuthorsRepository, AuthorsRepository>(provider => new AuthorsRepository(connectionString));

            services.AddControllersWithViews();
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(UserIdFilter));
                options.ValueProviderFactories.Add(new CookieValueProviderFactory());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
