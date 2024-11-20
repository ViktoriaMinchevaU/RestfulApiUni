using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = default!;

        [EmailAddress]
        public string Email { get; set; } = default!;

        // Navigation property
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}