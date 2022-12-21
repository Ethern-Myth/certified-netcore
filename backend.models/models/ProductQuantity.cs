using System.ComponentModel.DataAnnotations;

namespace backend.models.models;
public class ProductQuantity
{
    [Required]
    public int Quantity { get; set; }
    public double ProductTotal
    {
        get => Product.Price * Quantity;
        set { }
    }
    public virtual Product Product { get; set; }

    public ProductQuantity(
        int quantity,
        Product product)
    {
        Quantity = quantity;
        Product = product;
    }
}
