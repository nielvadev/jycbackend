//using jycbackend.Data;
//using jycbackend.DTOs;
//using jycbackend.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace jycbackend.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class DeliveriesController : ControllerBase
//    {
//        private readonly ApplicationDbContext _context;
//        public DeliveriesController(ApplicationDbContext context) => _context = context;

//        // GET: api/deliveries
//        [HttpGet]
//        public async Task<ActionResult<ApiResponse<IEnumerable<OrderDto>>>> GetOrders()
//        {
//            var orders = await _context.Orders
//                .Include(o => o.Client)
//                .Include(o => o.OrderDetails)
//                    .ThenInclude(od => od.Product)
//                .Select(o => new OrderDto
//                {
//                    Id = o.Id,
//                    OrderDate = o.OrderDate,
//                    EstimatedDeliveryDate = o.EstimatedDeliveryDate,
//                    Client = new ClientDto
//                    {
//                        Id = o.Client.Id,
//                        Name = o.Client.Name,
//                        Email = o.Client.Email
//                    },
//                    OrderDetails = o.OrderDetails.Select(od => new OrderDetailDto
//                    {
//                        Id = od.Id,
//                        ProductId = od.ProductId,
//                        Quantity = od.Quantity,
//                        ProductName = od.Product.Name,
//                        ProductPrice = od.Product.Price
//                    }).ToList()
//                })
//                .ToListAsync();

//            return Ok(new ApiResponse<IEnumerable<OrderDto>>("true", orders, "Órdenes obtenidas correctamente"));
//        }

//        // GET: api/deliveries/{id}
//        [HttpGet("{id}")]
//        public async Task<ActionResult<ApiResponse<OrderDto>>> GetOrder(Guid id)
//        {
//            var order = await _context.Orders
//                .Include(o => o.Client)
//                .Include(o => o.OrderDetails)
//                    .ThenInclude(od => od.Product)
//                .FirstOrDefaultAsync(o => o.Id == id);

//            if (order == null)
//                return NotFound(new ApiResponse<OrderDto>("false", null!, "Orden no encontrada"));

//            var dto = new OrderDto
//            {
//                Id = order.Id,
//                OrderDate = order.OrderDate,
//                EstimatedDeliveryDate = order.EstimatedDeliveryDate,
//                Client = new ClientDto
//                {
//                    Id = order.Client.Id,
//                    Name = order.Client.Name,
//                    Email = order.Client.Email
//                },
//                OrderDetails = order.OrderDetails.Select(od => new OrderDetailDto
//                {
//                    Id = od.Id,
//                    ProductId = od.ProductId,
//                    Quantity = od.Quantity,
//                    ProductName = od.Product.Name,
//                    ProductPrice = od.Product.Price
//                }).ToList()
//            };

//            return Ok(new ApiResponse<OrderDto>("true", dto, "Orden obtenida correctamente"));
//        }
//    }
//}
