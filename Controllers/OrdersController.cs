using jycbackend.Data;
using jycbackend.DTOs;
using jycbackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace jycbackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public OrdersController(ApplicationDbContext context) => _context = context;

        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<OrderDto>>>> GetOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.Client)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    EstimatedDeliveryDate = o.EstimatedDeliveryDate,
                    Client = new ClientDto
                    {
                        Id = o.Client.Id,
                        ClientDoc = o.Client.ClientDoc,
                        FirstName = o.Client.FirstName,
                        LastName = o.Client.LastName,
                        Email = o.Client.Email,
                        Phone = o.Client.Phone
                    },
                    OrderDetails = o.OrderDetails.Select(od => new OrderDetailDto
                    {
                        Id = od.Id,
                        ProductId = od.ProductId,
                        Quantity = od.Quantity,
                        ProductName = od.Product.Name,
                        ProductPrice = od.Product.Price
                    }).ToList()
                })
                .ToListAsync();

            return Ok(new ApiResponse<IEnumerable<OrderDto>>(true, orders, "Órdenes obtenidas correctamente"));
        }

        // GET: api/orders/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<OrderDto>>> GetOrder(Guid id)
        {
            var order = await _context.Orders
                .Include(o => o.Client)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return NotFound(new ApiResponse<OrderDto>(false, null!, "Orden no encontrada"));

            var dto = new OrderDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                EstimatedDeliveryDate = order.EstimatedDeliveryDate,
                Client = new ClientDto
                {
                    Id = order.Client.Id,
                    ClientDoc = order.Client.ClientDoc,
                    FirstName = order.Client.FirstName,
                    LastName = order.Client.LastName,
                    Email = order.Client.Email,
                    Phone = order.Client.Phone
                },
                OrderDetails = order.OrderDetails.Select(od => new OrderDetailDto
                {
                    Id = od.Id,
                    ProductId = od.ProductId,
                    Quantity = od.Quantity,
                    ProductName = od.Product.Name,
                    ProductPrice = od.Product.Price
                }).ToList()
            };

            return Ok(new ApiResponse<OrderDto>(true, dto, "Orden obtenida correctamente"));
        }

        // POST: api/orders
        [HttpPost]
        public async Task<ActionResult<ApiResponse<OrderDto>>> CreateOrder(CreateOrderDto dto)
        {
            var client = await _context.Clients.FindAsync(dto.ClientId);
            if (client == null)
                return NotFound(new ApiResponse<OrderDto>(false, null!, "Cliente no encontrado"));

            var order = new Order
            {
                ClientId = dto.ClientId,
                OrderDate = DateTime.UtcNow,
                EstimatedDeliveryDate = CalculateEstimatedDeliveryDate(DateTime.UtcNow),
                OrderDetails = dto.OrderDetails.Select(od => new OrderDetail
                {
                    ProductId = od.ProductId,
                    Quantity = od.Quantity,
                    UnitPrice = od.UnitPrice
                }).ToList()
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var createdOrder = await _context.Orders
                .Include(o => o.Client)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.Id == order.Id);

            var result = new OrderDto
            {
                Id = createdOrder!.Id,
                OrderDate = createdOrder.OrderDate,
                EstimatedDeliveryDate = createdOrder.EstimatedDeliveryDate,
                Client = new ClientDto
                {
                    Id = createdOrder.Client.Id,
                    ClientDoc = createdOrder.Client.ClientDoc,
                    FirstName = createdOrder.Client.FirstName,
                    LastName = createdOrder.Client.LastName,
                    Email = createdOrder.Client.Email,
                    Phone = createdOrder.Client.Phone
                },
                OrderDetails = createdOrder.OrderDetails.Select(od => new OrderDetailDto
                {
                    Id = od.Id,
                    ProductId = od.ProductId,
                    Quantity = od.Quantity,
                    ProductName = od.Product.Name,
                    ProductPrice = od.Product.Price
                }).ToList()
            };

            return CreatedAtAction(nameof(GetOrder), new { id = result.Id }, new ApiResponse<OrderDto>(true, result, "Orden creada correctamente"));
        }

        // PUT: api/orders/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<OrderDto>>> UpdateOrder(Guid id, CreateOrderDto dto)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return NotFound(new ApiResponse<OrderDto>(false, null!, "Orden no encontrada"));

            order.OrderDetails.Clear();
            order.OrderDetails = dto.OrderDetails.Select(od => new OrderDetail
            {
                ProductId = od.ProductId,
                Quantity = od.Quantity
            }).ToList();
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse<OrderDto>(true, null!, "Orden actualizada correctamente"));
        }

        // DELETE: api/orders/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteOrder(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return NotFound(new ApiResponse<string>(false, null!, "Orden no encontrada"));

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse<string>(true, "Orden eliminada correctamente", "Orden eliminada correctamente"));
        }

        private DateTime CalculateEstimatedDeliveryDate(DateTime orderDate)
        {
            return orderDate.AddDays(2);
        }
    }
}