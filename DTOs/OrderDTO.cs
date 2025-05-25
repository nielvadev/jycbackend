using System.ComponentModel.DataAnnotations;

namespace jycbackend.DTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public Guid ClientId { get; set; }
        public string? ClientName { get; set; }
        public List<OrderDetailDto> Details { get; set; } = new();

        // 🔧 Nuevas propiedades necesarias
        public ClientDto Client { get; set; } = default!;
        public List<OrderDetailDto> OrderDetails { get; set; } = new();
    }



    public class CreateOrderDto
    {
        [Required]
        public Guid ClientId { get; set; }
        [Required]
        public List<CreateOrderDetailDto> OrderDetails { get; set; } = new();
    }
}
