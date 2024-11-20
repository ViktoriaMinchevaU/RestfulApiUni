using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public int ProductCount { get; set; }
    }

    public class CategoryDetailDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public List<ProductDTO> Products { get; set; } = new List<ProductDTO>();
    }

    public class CategoryCreateDTO
    {
        [Required]
        public string Name { get; set; } = default!;
    }

    public class CategoryUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = default!;
    }
}