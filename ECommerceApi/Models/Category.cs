using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = default!;

        // Navigation property
        public List<Product> Products { get; set; } = new List<Product>();
    }
}