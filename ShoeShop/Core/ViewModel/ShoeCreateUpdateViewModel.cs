using ShoeShop.Core.Dto;
using ShoeShop.Core.Models;
using System.Collections.Generic;

namespace ShoeShop.Core.ViewModel
{
    public class ShoeCreateUpdateViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public ShoeDto ShoeDto { get; set; }

        public ShoeCreateUpdateViewModel()
        {
            Categories = new List<Category>();
        }
    }
}
