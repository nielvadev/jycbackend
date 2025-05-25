namespace jycbackend.Models
{
    public class Delivery
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OrderId { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public bool Delivered { get; set; } = false;
        public string? Observations { get; set; }

        public Order Order { get; set; } = null!;
    }

}
