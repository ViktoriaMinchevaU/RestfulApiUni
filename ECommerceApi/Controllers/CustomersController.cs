using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceApi.Data;
using EcommerceApi.DTOs;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly EcommerceContext _context;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(EcommerceContext context, ILogger<CustomersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers()
        {
            var customers = await _context.Customers
                .Include(c => c.Orders)
                .ToListAsync();

            var customerDTOs = customers.Select(c => new CustomerDTO
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                OrderCount = c.Orders.Count
            });

            return Ok(customerDTOs);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDetailDTO>> GetCustomer(int id)
        {
            var customer = await _context.Customers
                .Include(c => c.Orders)
                .ThenInclude(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
            {
                return NotFound(new { Message = $"Customer with id {id} not found" });
            }

            var customerDTO = new CustomerDetailDTO
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Orders = customer.Orders.Select(o => new OrderDTO
                {
                    Id = o.Id,
                    OrderPlaced = o.OrderPlaced,
                    TotalItems = o.OrderItems.Sum(oi => oi.Quantity),
                    TotalAmount = o.OrderItems.Sum(oi => oi.Quantity * oi.Product.Price)
                }).ToList()
            };

            return Ok(customerDTO);
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> PostCustomer(CustomerCreateDTO customerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = new Models.Customer
            {
                Name = customerDTO.Name,
                Email = customerDTO.Email
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            var resultDTO = new CustomerDTO
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                OrderCount = 0
            };

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, resultDTO);
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, CustomerUpdateDTO customerDTO)
        {
            if (id != customerDTO.Id)
            {
                return BadRequest(new { Message = "Customer ID mismatch" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound(new { Message = $"Customer with id {id} not found" });
            }

            customer.Name = customerDTO.Name;
            customer.Email = customerDTO.Email;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
            {
                return NotFound(new { Message = $"Customer with id {id} not found" });
            }

            if (customer.Orders.Any())
            {
                return BadRequest(new { Message = "Cannot delete customer with associated orders" });
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Customers/ByProduct/5
        [HttpGet("ByProduct/{productId}")]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomersByProduct(int productId)
        {
            _logger.LogInformation($"Getting customers who bought product with id {productId}");

            var productExists = await _context.Products.AnyAsync(p => p.Id == productId);
            if (!productExists)
            {
                return NotFound(new { Message = $"Product with id {productId} not found" });
            }

            var customers = await _context.Customers
                .Include(c => c.Orders)
                    .ThenInclude(o => o.OrderItems)
                .Where(c => c.Orders.Any(o => o.OrderItems.Any(oi => oi.ProductId == productId)))
                .ToListAsync();

            var customerDTOs = customers.Select(c => new CustomerDTO
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                OrderCount = c.Orders.Count
            });

            return Ok(customerDTOs);
        }
    }
}
