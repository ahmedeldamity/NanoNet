namespace NanoNet.Services.OrderAPI.Dtos;
public class StripeRequestDto
{
    public string? StripeSessionUrl { get; set; } = null!;
    public string? StripeSessionId { get; set; } = null!;
    public string StripeApprovedUrl { get; set; } = null!;
    public string StripeCancelledUrl { get; set; } = null!;
    public OrderHeaderDto OrderHeader { get; set; } = null!;
}