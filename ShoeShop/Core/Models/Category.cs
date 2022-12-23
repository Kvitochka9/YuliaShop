using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ShoeShop.Core.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }


        public ICollection<Shoe> Shoes { get; set; }

        public Category()
        {
            Shoes = new Collection<Shoe>();
        }
    }
}
