using System.ComponentModel.DataAnnotations;

namespace ShoeShop.Core.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string ShoeName { get; set; }
        public int Qty { get; set; }

        /// Adding SIZE
        public int Size { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }

        public Order Order { get; set; }
    }
}
