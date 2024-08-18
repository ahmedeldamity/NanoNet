using System.ComponentModel.DataAnnotations.Schema;

namespace NanoNet.Services.ShoppingCartAPI.Models
{
    public class CartHeader
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }

        [NotMapped]
        public double Discount { get; set; }
        [NotMapped]
        public double TotalPrice { get; set; }
    }
}
