namespace NanoNet.Web.ViewModels;
public class StripeRequestViewModel
{
    public string StripeSessionUrl { get; set; } = null!;
    public string StripeSessionId { get; set; } = null!;
    public string StripeApprovedUrl { get; set; } = null!;
    public string StripeCancelUrl { get; set; } = null!;
    public OrderHeaderViewModel OrderHeader { get; set; } = null!;
}