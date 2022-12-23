using System.Threading.Tasks;

namespace ShoeShop.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
