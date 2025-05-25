namespace jycbackend.Models
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public Guid ClientId { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        public string Status { get; set; } = "Pending";

        public Client Client { get; set; } = null!;
        public List<OrderDetail> OrderDetails { get; set; } = new();
        public Delivery? Delivery { get; set; }
    }

}
