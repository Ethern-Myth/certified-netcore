using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace backend.models.requests;
public class ProductRequest
{
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
    [Required]
    public string Brand { get; set; }
    [Required]
    public double Price { get; set; }
    [Required]
    public bool InStock { get; set; }
    [Required]
    public int PDTypeID { get; set; }
    public IFormFile? Image { get; set; }
}
