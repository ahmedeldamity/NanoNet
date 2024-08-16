using System.ComponentModel.DataAnnotations;

namespace NanoNet.Web.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        [Display(Name = "Image Url")]
        public string? ImageUrl { get; set; }
    }
}
