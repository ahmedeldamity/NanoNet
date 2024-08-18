namespace NanoNet.Services.ShoppingCartAPI.Dtos
{
    public class CartDto
    {
        public CartHeaderDto CartHeader { get; set; }
        public IEnumerable<CartItemDto> CartItems { get; set; } = [];
    }
}
