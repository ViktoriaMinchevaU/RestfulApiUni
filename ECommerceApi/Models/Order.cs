using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderPlaced { get; set; } = DateTime.UtcNow;

        // Foreign Key
        [Required]
        public int CustomerId { get; set; }

        // Navigation property
        [Required]
        public Customer Customer { get; set; } = default!;

        // Navigation property
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}