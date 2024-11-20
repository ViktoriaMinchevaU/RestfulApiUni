using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be positive")]
        public decimal Price { get; set; }

        // Foreign Key
        [Required]
        public int CategoryId { get; set; }

        // Navigation property
        [Required]
        public Category Category { get; set; } = default!;
    }
}
