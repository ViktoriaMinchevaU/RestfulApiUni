using Microsoft.EntityFrameworkCore;
using EcommerceApi.Models;

namespace EcommerceApi.Data
{
    public class EcommerceContext : DbContext
    {
        public EcommerceContext(DbContextOptions<EcommerceContext> options)
            : base(options)
        { 
        }

        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;   
        public DbSet<Customer> Customers { get; set; } = default!;   
        public DbSet<Order> Orders { get; set; } = default!;   
        public DbSet<OrderItem> OrderItems { get; set; } = default!;
    }
}
