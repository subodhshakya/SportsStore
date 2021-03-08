using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsStore.Models;

namespace SportsStore
{
    /// <summary>
    /// This class is responsible for configuring the ASP.NET core
    /// application.
    /// </summary>
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
            services.AddControllersWithViews();

            // Register database context class
            services.AddDbContext<StoreDbContext>(opts =>
            {
                opts.UseSqlServer(Configuration["ConnectionStrings:SportsStoreConnection"]);
            });
            services.AddScoped<IStoreRepository, EFStoreRepository>();
            services.AddRazorPages(); // Sets up the services used by Razor pages
            services.AddDistributedMemoryCache(); // Sets up the in-memory data store
            services.AddSession(); // Method registers the services used to access session data, and            
            
            // Specifies that same object should be used to satisfy related requests for Cart instances.
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp)); 
            
            // Also specifies that object should always be used.
            // This service is required so that I can access the current session in the SessionCart class.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // This extension method displays details of exceptions that occur in the application, which is
                // useful during the development process. It shouldn't be enabled in deployed applications.
                // This will be disabled when deployed in Ch 11.
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            
            // This extension method adds a simple message to HTTP responses 
            // that would not otherwise have a body, such as 404 - Not found responses
            app.UseStatusCodePages();
            
            // This extension method enables support for serving static content from the
            // wwwroot folder. More details in Ch 15
            app.UseStaticFiles();

            // To store details of a user's cart--session state which is data associated with a series of 
            // requests made by a user
            // This method allows the session system to automatically associate requests with sessions
            // when they arrive from the client.
            app.UseSession();

            // The endpoint routing feature is added to the request pipeline with the UseRouting
            // and UseEndpoints
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                #region To accomodate category sidebar
                endpoints.MapControllerRoute("catpage",
                    "{category}/Page{productPage:int}",
                    new { Controller = "Home", action = "Index" });
                endpoints.MapControllerRoute("page", "Page{productPage:int}",
                    new { Controller = "Home", action = "Index", productPage = 1 });
                endpoints.MapControllerRoute("page", "{category}",
                    new { Controller = "Home", action = "Index", productPage = 1 });
                #endregion

                // This new route is to make URLs more appealing
                // by creating a composable URLs scheme
                endpoints.MapControllerRoute("pagination", 
                    "Products/Page{productPage}",
                    new { Controller = "Home", action = "Index" });
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                // MapRazorPages Registers Razor pages as endpoints that the URL routing system can use to handle requests
                endpoints.MapRazorPages(); 
            });
            SeedData.EnsurePopulated(app);
        }
    }
}
