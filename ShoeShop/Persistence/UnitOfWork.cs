using ShoeShop.Core;
using System.Threading.Tasks;

namespace ShoeShop.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShoeShopDbContext _context;

        public UnitOfWork(ShoeShopDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync() =>
            await _context.SaveChangesAsync();
    }
}
