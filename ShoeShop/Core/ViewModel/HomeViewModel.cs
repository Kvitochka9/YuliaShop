using ShoeShop.Core.Models;
using System.Collections.Generic;

namespace ShoeShop.Core.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<Shoe> ShoeOfTheWeek { get; set; }
    }
}
