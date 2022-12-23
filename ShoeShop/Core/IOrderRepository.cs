using ShoeShop.Core.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoeShop.Core.Models
{
    public interface IOrderRepository
    {
        Task CreateOrderAsync(Order order);
        Task<IEnumerable<MyOrderViewModel>> GetAllOrdersAsync();
        Task<IEnumerable<MyOrderViewModel>> GetAllOrdersAsync(string userId);
    }
}