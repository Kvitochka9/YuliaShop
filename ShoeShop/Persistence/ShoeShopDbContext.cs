using ShoeShop.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ShoeShop.Persistence
{
    public class ShoeShopDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public ShoeShopDbContext(DbContextOptions<ShoeShopDbContext> options)
            : base(options)
        {

        }
    }
}
