using System.ComponentModel.DataAnnotations;

namespace NanoNet.Web.ViewModels;
public class CartHeaderViewModel
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public string? CouponCode { get; set; }
    public double Discount { get; set; }
    public double TotalPrice { get; set; }

    [Required]
    [Display(Name = "Full Name")]
    public string? Name { get; set; }

    [Required]
    [Display(Name = "Email Address")]
    public string? Email { get; set; }

    [Required]
    [Display(Name = "Phone Number")]
    public string? Phone { get; set; }
}