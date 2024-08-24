namespace NanoNet.Services.EmailAPI.Dtos;
public class CartDto
{
    public CartHeaderDto CartHeader { get; set; } = new();
    public IEnumerable<CartItemDto> CartItems { get; set; } = [];
}