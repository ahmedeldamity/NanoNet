namespace NanoNet.Services.ShoppingCartAPI.Dtos;
public class ResponseDto
{
    public bool IsSuccess { get; set; } = true;
    public object? Value { get; set; }
    public string Error { get; set; } = null!;
}