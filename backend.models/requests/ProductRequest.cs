using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace backend.models.requests;
public class ProductRequest
{
    [Required(ErrorMessage = "Product name is required")]
    public string Name { get; set; }
    public string? Description { get; set; }
    [Required(ErrorMessage = "Product name is required")]
    public string Brand { get; set; }
    [Required(ErrorMessage = "Price is required")]
    [Range(0, int.MaxValue)]
    public double Price { get; set; }
    [Required(ErrorMessage = "In stock is required")]
    public bool InStock { get; set; }
    [Required(ErrorMessage = "Product Type ID is required")]
    public int PDTypeID { get; set; }
    public IFormFile? Image { get; set; }
}
