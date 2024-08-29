using NanoNet.Services.OrderAPI.Dtos;
using System.ComponentModel.DataAnnotations.Schema;

namespace NanoNet.Services.OrderAPI.Models
{
    public class OrderItems
    {
        public int Id { get; set; }
        public int OrderHeaderId { get; set; }
        public OrderHeader? OrderHeader { get; set; }
        public int ProductId { get; set; }

        [NotMapped]
        public ProductDto? Product { get; set; }
        public int Count { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
    }
}
