using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        // Foreign Keys
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }

        // Quantity of the product in the order
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        // Navigation properties
        [Required]
        public Order Order { get; set; } = default!;
        [Required]
        public Product Product { get; set; } = default!;
    }
}