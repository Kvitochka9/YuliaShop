using ShoeShop.Core.Dto;
using ShoeShop.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeShop.Persistence
{
    public class ShoeRepository : IShoeRepository
    {
        private readonly ShoeShopDbContext _context;

        public ShoeRepository(ShoeShopDbContext context)
        {
            _context = context;
        }


        public async Task<Shoe> GetShoeById(int shoeId)
        {
            return await _context.Shoes.FirstAsync(e => e.Id == shoeId);
        }


        public async Task<IEnumerable<Shoe>> GetShoes(string category = null)
        {
            var query = _context.Shoes
                .Include(c => c.Category)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(c => c.Category.Name == category);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Shoe>> GetShoesOfTheWeek()
        {
            return await _context.Shoes
                .Where(e => e.IsShoeOfTheWeek)
                .Include(e => e.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<ShoeNameIdDto>> GetAllShoesNameId()
        {
            return await _context.Shoes
                 .Select(e => new ShoeNameIdDto
                 {
                     Id = e.Id,
                     Name = e.Name
                 })
                 .ToListAsync();
        }
        

        public void UpdateShoe(Shoe shoe)
        {
            _context.Shoes.Update(shoe);
        }

        public async Task AddShoeAsync(Shoe shoe)
        {
            await _context.Shoes.AddAsync(shoe);
        }

        public void Delete(int id)
        {
            var shoe = new Shoe { Id = id };
            _context.Entry(shoe).State = EntityState.Deleted;
        }
    }
}
