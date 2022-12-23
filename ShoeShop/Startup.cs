using AutoMapper;
using ShoeShop.Core;
using ShoeShop.Core.Models;
using ShoeShop.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ShoeShop
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<IShoeRepository, ShoeRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IShoppingCartService>(sp => ShoppingCartService.GetCart(sp));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //services.AddDbContext<ShoeShopDbContext>(ctx =>
            //{
            //    ctx.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            //});

            //////////////// ADDED AZURE SQL ////////////////
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
                services.AddDbContext<ShoeShopDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AzureSQL")));
            else
                services.AddDbContext<ShoeShopDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //services.BuildServiceProvider().GetService<ShoeShopDbContext>().Database.Migrate();

            services.AddAutoMapper();
            services.AddMemoryCache();

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<ShoeShopDbContext>();

            //services.AddSession();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/UnAuthorized";
            });
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseStatusCodePages();
            //app.UseSession();
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name: "categoryFilter",
                //    template: "Shoes/{action}/{category?}",
                //    defaults: new { Controller = "Shoe", action = "List" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
