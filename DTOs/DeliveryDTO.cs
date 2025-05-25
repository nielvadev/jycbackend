using System.ComponentModel.DataAnnotations;

namespace jycbackend.DTOs
{
    public class DeliveryDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public bool Delivered { get; set; }
        public string? Observations { get; set; }
    }

    public class CreateDeliveryDto
    {
        [Required]
        public Guid OrderId { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public bool Delivered { get; set; } = false;
        public string? Observations { get; set; }
    }

}
