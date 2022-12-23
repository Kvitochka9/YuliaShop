using ShoeShop.Core.Dto;
using ShoeShop.Core.Models;
using ShoeShop.Core.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeShop.Persistence
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShoeShopDbContext _context;
        private readonly IShoppingCartService _shoppingCartService;

        public OrderRepository(
            ShoeShopDbContext context,
            IShoppingCartService shoppingCartService)
        {
            _context = context;
            _shoppingCartService = shoppingCartService;
        }

        public async Task CreateOrderAsync(Order order)
        {
            order.OrderPlacedTime = DateTime.Now;
            await _context.Orders.AddAsync(order);

            var shoppingCartItems = await _shoppingCartService.GetShoppingCartItemsAsync();
            order.OrderTotal = (await _shoppingCartService.GetCartCountAndTotalAmmountAsync()).TotalAmmount;

            await _context.OrderDetails.AddRangeAsync(shoppingCartItems.Select(e => new OrderDetail
            {
                Qty = e.Qty,
                ShoeName = e.Shoe.Name,
                //////////////Adding size//////////////
                Size = e.Size,
                OrderId = order.Id,
                Price = e.Shoe.Price
            }));

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MyOrderViewModel>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(e => e.OrderDetails)
                .Select(e => new MyOrderViewModel
                {
                    OrderPlacedTime = e.OrderPlacedTime,
                    OrderTotal = e.OrderTotal,
                    OrderPlaceDetails = new OrderDto
                    {
                        AddressLine1 = e.AddressLine1,
                        AddressLine2 = e.AddressLine2,
                        City = e.City,
                        Country = e.Country,
                        Email = e.Email,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        PhoneNumber = e.PhoneNumber,
                        State = e.State,
                        ZipCode = e.ZipCode
                    },
                    ShoeOrderInfos = e.OrderDetails.Select(o => new MyShoeOrderInfo
                    { 
                        Name = o.ShoeName,
                        //////////////Adding size//////////////
                        Size = o.Size,
                        Price = o.Price,
                        Qty = o.Qty
                    })
                })
                .ToListAsync();

        }
        public async Task<IEnumerable<MyOrderViewModel>> GetAllOrdersAsync(string userId)
        {
            return await _context.Orders
                .Where(e => e.UserId == userId)
                .Include(e => e.OrderDetails)
                .Select(e => new MyOrderViewModel
                {
                    OrderPlacedTime = e.OrderPlacedTime,
                    OrderTotal = e.OrderTotal,
                    OrderPlaceDetails = new OrderDto
                    {
                        AddressLine1 = e.AddressLine1,
                        AddressLine2 = e.AddressLine2,
                        City = e.City,
                        Country = e.Country,
                        Email = e.Email,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        PhoneNumber = e.PhoneNumber,
                        State = e.State,
                        ZipCode = e.ZipCode
                    },
                    ShoeOrderInfos = e.OrderDetails.Select(o => new MyShoeOrderInfo
                    {
                        Name = o.ShoeName,
                        //////////////Adding size//////////////
                        Size = o.Size,
                        Price = o.Price,
                        Qty = o.Qty
                    })
                })
                .ToListAsync();
        }
    }
}
