namespace NanoNet.Web.ViewModels;
public class OrderHeaderViewModel
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public string? CouponCode { get; set; }
    public double Discount { get; set; }
    public double TotalPrice { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public DateTime OrderTime { get; set; } = DateTime.Now;
    public string? Status { get; set; }
    public string? PaymentIntentId { get; set; }
    public string? StripeSessionId { get; set; }
    public IEnumerable<OrderItemsViewModel> OrderItems { get; set; }
}