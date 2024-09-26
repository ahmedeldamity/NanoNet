namespace NanoNet.Web.ViewModels;
public class OrderItemsViewModel
{
    public int Id { get; set; }
    public int OrderHeaderId { get; set; }
    public int ProductId { get; set; }
    public ProductViewModel? Product { get; set; }
    public int Count { get; set; }
    public string ProductName { get; set; } = null!;
    public double Price { get; set; }
}