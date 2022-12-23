using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoeShop.Core.Models
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
    }
}
