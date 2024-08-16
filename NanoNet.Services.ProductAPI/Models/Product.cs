namespace NanoNet.Services.ProductAPI.Models
{
    public class Product: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryName { get; set; }
    }
}
