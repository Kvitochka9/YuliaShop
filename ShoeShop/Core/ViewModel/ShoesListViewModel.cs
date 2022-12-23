using ShoeShop.Core.Models;
using System.Collections.Generic;

namespace ShoeShop.Core.ViewModel
{
    public class ShoesListViewModel
    {
        public IEnumerable<Shoe> Shoes { get; set; }
        public string CurrentCategory { get; set; }
    }
}
