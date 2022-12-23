using ShoeShop.Core.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoeShop.Core.Models
{
    public interface IShoeRepository
    {
        Task<IEnumerable<Shoe>> GetShoes(string category = null);
        Task<IEnumerable<Shoe>> GetShoesOfTheWeek();

        Task<Shoe> GetShoeById(int shoeId);

        Task<IEnumerable<ShoeNameIdDto>> GetAllShoesNameId();

        void UpdateShoe(Shoe shoe);
        Task AddShoeAsync(Shoe shoe);
        void Delete(int id);
    }
}
