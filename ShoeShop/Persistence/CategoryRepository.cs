using ShoeShop.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoeShop.Persistence
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ShoeShopDbContext _context;

        public CategoryRepository(ShoeShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
