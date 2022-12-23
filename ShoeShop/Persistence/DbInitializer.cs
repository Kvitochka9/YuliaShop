using ShoeShop.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace ShoeShop.Persistence
{
    public static class DbInitializer
    {
        public static void SeedDatabase(
            ShoeShopDbContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            System.Console.WriteLine("Seeding... - Start");

            var categories = new List<Category>
            {
                new Category { Name = "Повсякденне", Description = "Повсякденне взуття на кожен день."},
                new Category { Name = "Спортивне", Description = "Для занять спортом, активного відпочинку."},
                new Category { Name = "Класичне", Description = "На випускний чи на вручення дипломів!:)"},
                new Category { Name = "Для хайкінгу", Description = "Для хайкінгу/аутдору, в подорожі."},
                new Category { Name = "Дитяче", Description = "Для наших найдорожчих діток."},
                new Category { Name = "Сандалі/Пантолети", Description = "На літо саме то!"},
                new Category { Name = "Футбольне", Description = "Хто гратиме у футбол у найкращому взутті? Ви!"},
                new Category { Name = "Розкішне", Description = "Лакшері чи лухурі? Обирати вам!"},
                new Category { Name = "Ортопедичне", Description = "Подбає про ваші стопи!"},

            };
            var shoes = new List<Shoe>
            {
                new Shoe
                {
                    Name ="Burberry",
                    Price = 28900.00M,
                    ShortDescription ="So cool",
                    LongDescription ="It's Burberry, baby",
                    Category = categories[0],
                    ImageUrl ="/img/Shoes/Luxury/burberry.webp",
                    IsShoeOfTheWeek = true,
                }
            };

            if (!context.Categories.Any() && !context.Shoes.Any())
            {
                context.Categories.AddRange(categories);
                context.Shoes.AddRange(shoes);
                context.SaveChanges();
            }

            IdentityUser usr = null;
            string userEmail = configuration["Admin:Email"] ?? "admin@admin.com";
            string userName = configuration["Admin:Username"] ?? "admin";
            string password = configuration["Admin:Password"] ?? "Passw@rd!123";

            if (!context.Users.Any())
            {
                usr = new IdentityUser
                {
                    Email = userEmail,
                    UserName = userName
                };
                userManager.CreateAsync(usr, password);
            }

            if (!context.UserRoles.Any())
            {
                roleManager.CreateAsync(new IdentityRole("Admin"));

            }
            if (usr == null)
            {
                usr = userManager.FindByEmailAsync(userEmail).Result;
            }
            if (!userManager.IsInRoleAsync(usr, "Admin").Result)
            {
                userManager.AddToRoleAsync(usr, "Admin");
            }

            context.SaveChanges();
            System.Console.WriteLine("Seeding... - End");
        }

    }
}