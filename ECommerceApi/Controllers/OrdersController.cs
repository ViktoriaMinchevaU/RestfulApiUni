using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceApi.Data;
using EcommerceApi.DTOs;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly EcommerceContext _context;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(EcommerceContext context, ILogger<OrdersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .ToListAsync();

            var orderDTOs = orders.Select(o => new OrderDTO
            {
                Id = o.Id,
                OrderPlaced = o.OrderPlaced,
                TotalItems = o.OrderItems.Sum(oi => oi.Quantity),
                TotalAmount = o.OrderItems.Sum(oi => oi.Quantity * oi.Product.Price)
            });

            return Ok(orderDTOs);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailDTO>> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound(new { Message = $"Order with id {id} not found" });
            }

            var orderDTO = new OrderDetailDTO
            {
                Id = order.Id,
                OrderPlaced = order.OrderPlaced,
                CustomerName = order.Customer.Name,
                Items = order.OrderItems.Select(oi => new OrderItemDTO
                {
                    Id = oi.Id,
                    ProductId = oi.ProductId,
                    ProductName = oi.Product.Name,
                    Quantity = oi.Quantity,
                    Price = oi.Product.Price,
                    TotalPrice = oi.Quantity * oi.Product.Price
                }).ToList()
            };

            return Ok(orderDTO);
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> PostOrder(OrderCreateDTO orderDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customers.FindAsync(orderDTO.CustomerId);
            if (customer == null)
            {
                return NotFound(new { Message = $"Customer with id {orderDTO.CustomerId} not found" });
            }

            var order = new Models.Order
            {
                CustomerId = orderDTO.CustomerId,
                OrderPlaced = DateTime.UtcNow,
                OrderItems = new List<Models.OrderItem>()
            };

            foreach (var itemDTO in orderDTO.Items)
            {
                var product = await _context.Products.FindAsync(itemDTO.ProductId);
                if (product == null)
                {
                    return NotFound(new { Message = $"Product with id {itemDTO.ProductId} not found" });
                }

                order.OrderItems.Add(new Models.OrderItem
                {
                    ProductId = itemDTO.ProductId,
                    Quantity = itemDTO.Quantity
                });
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var resultDTO = new OrderDTO
            {
                Id = order.Id,
                OrderPlaced = order.OrderPlaced,
                TotalItems = order.OrderItems.Sum(oi => oi.Quantity),
                TotalAmount = order.OrderItems.Sum(oi => oi.Quantity * _context.Products.First(p => p.Id == oi.ProductId).Price)
            };

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, resultDTO);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound(new { Message = $"Order with id {id} not found" });
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Orders/ByCustomer/5
        [HttpGet("ByCustomer/{customerId}")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersByCustomer(int customerId)
        {
            _logger.LogInformation($"Getting orders for customer with id {customerId}");

            var customerExists = await _context.Customers.AnyAsync(c => c.Id == customerId);
            if (!customerExists)
            {
                return NotFound(new { Message = $"Customer with id {customerId} not found" });
            }

            var orders = await _context.Orders
                .Where(o => o.CustomerId == customerId)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .ToListAsync();

            var orderDTOs = orders.Select(o => new OrderDTO
            {
                Id = o.Id,
                OrderPlaced = o.OrderPlaced,
                TotalItems = o.OrderItems.Sum(oi => oi.Quantity),
                TotalAmount = o.OrderItems.Sum(oi => oi.Quantity * oi.Product.Price)
            });

            return Ok(orderDTOs);
        }
    }
}