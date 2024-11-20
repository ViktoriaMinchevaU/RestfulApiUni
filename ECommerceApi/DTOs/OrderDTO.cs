using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public DateTime OrderPlaced { get; set; }

        public int TotalItems { get; set; }

        public decimal TotalAmount { get; set; }
    }

    public class OrderDetailDTO
    {
        public int Id { get; set; }

        public DateTime OrderPlaced { get; set; }

        public string CustomerName { get; set; } = default!;

        public List<OrderItemDTO> Items { get; set; } = new List<OrderItemDTO>();
    }

    public class OrderCreateDTO
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public List<OrderItemCreateDTO> Items { get; set; } = new List<OrderItemCreateDTO>();
    }
}