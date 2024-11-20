using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public string CategoryName { get; set; } = default!;

        public decimal Price { get; set; }
    }

    public class ProductCreateDTO
    {
        [Required]
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be positive")]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }

    public class ProductUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be positive")]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}