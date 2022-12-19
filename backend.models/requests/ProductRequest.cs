using System.ComponentModel.DataAnnotations;

namespace backend.models.requests;

public struct ProductRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public double Price { get; set; }
    [Required]
    public bool InStock { get; set; }
    [Required]
    public int PDTypeID { get; set; }

    public ProductRequest()
    {
        Price = 0.00;
        InStock = true;
    }

}
