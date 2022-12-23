using ShoeShop.Core.Dto;
using System;
using System.Collections.Generic;

namespace ShoeShop.Core.ViewModel
{
    public class MyOrderViewModel
    {
        public OrderDto OrderPlaceDetails { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime OrderPlacedTime { get; set; }
        public IEnumerable<MyShoeOrderInfo> ShoeOrderInfos { get; set; }

    }

    public class MyShoeOrderInfo
    {
        public int Qty { get; set; }
        public decimal Price { get; set; }

        /// Adding SIZE
        public int Size { get; set; }

        public string Name { get; set; }
    }
}
