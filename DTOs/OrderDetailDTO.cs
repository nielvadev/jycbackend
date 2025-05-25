using System.ComponentModel.DataAnnotations;

namespace jycbackend.DTOs
{
    public class OrderDetailDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal ProductPrice { get; set; }
    }


    public class CreateOrderDetailDto
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required] 
        public decimal UnitPrice { get; set; }
    }

}
