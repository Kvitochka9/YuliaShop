using ShoeShop.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeShop.Persistence
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ShoeShopDbContext _context;

        public string Id { get; set; }
        public IEnumerable<ShoppingCartItem> ShoppingCartItems { get; set; }

        private ShoppingCartService(ShoeShopDbContext context)
        {
            _context = context;
        }

        public static ShoppingCartService GetCart(IServiceProvider services)
        {
            var httpContext = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext;
            var context = services.GetRequiredService<ShoeShopDbContext>();

            var request = httpContext.Request;
            var response = httpContext.Response;

            var cardId = request.Cookies["CartId-cookie"] ?? Guid.NewGuid().ToString();

            response.Cookies.Append("CartId-cookie", cardId, new CookieOptions
            {
                Expires = DateTime.Now.AddMonths(2)
            });

            return new ShoppingCartService(context)
            {
                Id = cardId
            };
        }

        ////////////TO ADD AN ITEM TO THE CART//////////////////
        public async Task<int> AddToCartAsync(Shoe shoe, int qty = 1)
        {
            return await AddOrRemoveCart(shoe, qty);
        }

        ////////////TO REMOVE AN ITEM TO THE CART//////////////////
        public async Task<int> RemoveFromCartAsync(Shoe shoe)
        {
            return await AddOrRemoveCart(shoe, -1);
        }

        ////////////TO GET ITEMS IN THE CART//////////////////
        public async Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItemsAsync()
        {
            ShoppingCartItems = ShoppingCartItems ?? await _context.ShoppingCartItems
                .Where(e => e.ShoppingCartId == Id)
                .Include(e => e.Shoe)
                .ToListAsync();

            return ShoppingCartItems;
        }

        //////BIGGER SIZE///////
        public async Task<int> BiggerSizeAsync(Shoe shoe, int size = 1)
        {
            return await AdjustSize(shoe, size);
        }
        //////SMALLER SIZE///////
        public async Task<int> SmallerSizeAsync(Shoe shoe)
        {
            return await AdjustSize(shoe, -1);
        }

        ////////////TO CLEAR THE CART//////////////////
        public async Task ClearCartAsync()
        {
            var shoppingCartItems = _context
                .ShoppingCartItems
                .Where(s => s.ShoppingCartId == Id);

            _context.ShoppingCartItems.RemoveRange(shoppingCartItems);

            ShoppingCartItems = null; //reset
            await _context.SaveChangesAsync();
        }

        public async Task<(int ItemCount, decimal TotalAmmount)> GetCartCountAndTotalAmmountAsync()
        {
            var subTotal = ShoppingCartItems?
                .Select(c => c.Shoe.Price * c.Qty) ??
                await _context.ShoppingCartItems
                .Where(c => c.ShoppingCartId == Id)
                .Select(c => c.Shoe.Price * c.Qty)
                .ToListAsync();

            return (subTotal.Count(), subTotal.Sum());
        }

        private async Task<int> AddOrRemoveCart(Shoe shoe, int qty)
        {
            var shoppingCartItem = await _context.ShoppingCartItems
                .SingleOrDefaultAsync(s => s.ShoeId == shoe.Id && s.ShoppingCartId == Id);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = Id,
                    Shoe = shoe,
                    Qty = 0,
                    ////SIZE/////
                    Size = 36
                };

                await _context.ShoppingCartItems.AddAsync(shoppingCartItem);
            }

            shoppingCartItem.Qty += qty;

            if (shoppingCartItem.Qty <= 0)
            {
                shoppingCartItem.Qty = 0;
                _context.ShoppingCartItems.Remove(shoppingCartItem);
            }

            await _context.SaveChangesAsync();

            ShoppingCartItems = null; // Reset

            return await Task.FromResult(shoppingCartItem.Qty);
        }

        public async Task<int> AdjustSize(Shoe shoe, int size)
//під другим атрибутом(size) деколи підкреслювало і пропонувало пульнути AdjustSize в IShoppingCartService.cs
        {
            var shoppingCartItem = await _context.ShoppingCartItems
                .SingleOrDefaultAsync(s => s.ShoeId == shoe.Id && s.ShoppingCartId == Id);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = Id,
                    Shoe = shoe,
                    Qty = 0,
                    ////SIZE/////
                    Size = 36
                };

                await _context.ShoppingCartItems.AddAsync(shoppingCartItem);
            }
            shoppingCartItem.Size += size;////////////////ця хуйня все порішала і сайз селектор запахав
            await _context.SaveChangesAsync();

            ShoppingCartItems = null; // Reset

            return await Task.FromResult(shoppingCartItem.Size);
            //throw new NotImplementedException();
        }
    }
}
