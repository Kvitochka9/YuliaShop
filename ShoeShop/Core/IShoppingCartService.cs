using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoeShop.Core.Models
{
    public interface IShoppingCartService
    {
        string Id { get; set; }
        IEnumerable<ShoppingCartItem> ShoppingCartItems { get; set; }

        Task<int> AddToCartAsync(Shoe shoe, int qty = 1);
        Task<int> RemoveFromCartAsync(Shoe shoe);

        ////////////ADJUST THE SIZE//////////////////
        Task<int> BiggerSizeAsync(Shoe shoe, int size = 1);
        Task<int> SmallerSizeAsync(Shoe shoe);

        Task ClearCartAsync();
        Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItemsAsync();
        
        Task<(int ItemCount, decimal TotalAmmount)> GetCartCountAndTotalAmmountAsync();
    }
}