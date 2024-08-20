namespace NanoNet.Web.ViewModels;
public class CartViewModel
{
    public CartHeaderDto CartHeader { get; set; }
    public IEnumerable<CartItemDto> CartItems { get; set; } = [];
}