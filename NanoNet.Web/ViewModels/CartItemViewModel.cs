namespace NanoNet.Web.ViewModels;
public class CartItemViewModel
{
    public int Id { get; set; }
    public int CartHeaderId { get; set; }
    public CartHeaderViewModel? CartHeader { get; set; }
    public int ProductId { get; set; }
    public ProductViewModel? Product { get; set; }
    public int Count { get; set; }
}