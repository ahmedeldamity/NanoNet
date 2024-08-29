using NanoNet.Services.OrderAPI.Models;

namespace NanoNet.Services.OrderAPI.Dtos
{
    public class OrderHeaderDto
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }
        public double Discount { get; set; }
        public double TotalPrice { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime OrderTime { get; set; }
        public string? Status { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? StripeSessionId { get; set; }
        public IEnumerable<OrderItems> OrderDetails { get; set; }
    }
}