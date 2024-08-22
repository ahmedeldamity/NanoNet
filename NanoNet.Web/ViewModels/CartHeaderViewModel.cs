namespace NanoNet.Web.ViewModels;
public class CartHeaderViewModel
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public string? CouponCode { get; set; }
    public double Discount { get; set; }
    public double TotalPrice { get; set; }
    public string? FirstName { get; set; }  
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}