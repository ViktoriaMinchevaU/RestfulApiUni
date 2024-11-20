using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string Email { get; set; } = default!;

        public int OrderCount { get; set; }
    }

    public class CustomerDetailDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string Email { get; set; } = default!;

        public List<OrderDTO> Orders { get; set; } = new List<OrderDTO>();
    }

    public class CustomerCreateDTO
    {
        [Required]
        public string Name { get; set; } = default!;

        [EmailAddress]
        public string Email { get; set; } = default!;
    }

    public class CustomerUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = default!;

        [EmailAddress]
        public string Email { get; set; } = default!;
    }
}