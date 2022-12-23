using System.ComponentModel.DataAnnotations;

namespace ShoeShop.Core.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }

        public int Qty { get; set; }

        /// Adding SIZE
        public int Size { get; set; }

        public int ShoeId { get; set; }

        public Shoe Shoe { get; set; }

        [Required]
        [StringLength(255)]
        public string ShoppingCartId { get; set; }
    }
}
