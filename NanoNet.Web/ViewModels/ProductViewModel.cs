using System.ComponentModel.DataAnnotations;

namespace NanoNet.Web.ViewModels;
public class ProductViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double Price { get; set; }
    public string Description { get; set; } = null!;
    [Display(Name = "Category")] public string CategoryName { get; set; } = null!;
    [Display(Name = "Image Url")] public string? ImageUrl { get; set; } = null!;
    [Range(1, 100)] public int Count { get; set; } = 1;
}