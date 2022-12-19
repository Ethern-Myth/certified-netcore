using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.models.models;

[Table("product")]
public class Product : BaseModel
{
    [Key]
    public Guid ProductID { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public double Price { get; set; } = 0.00;
    [Required]
    public bool InStock { get; set; } = true;

    [ForeignKey(nameof(ProductType))]
    public int PDTypeID { get; set; }
    public virtual ProductType? ProductType { get; set; }

    public Product() { }

    public Product(
        Guid productID,
        string name,
        double price,
        bool inStock,
        ProductType? productType,
        int pDTypeID)
    {
        ProductID = productID;
        Name = name;
        Price = price;
        InStock = inStock;
        ProductType = productType;
        PDTypeID = pDTypeID;
    }
    public Product(
        string name,
        double price,
        bool inStock,
        int pDTypeID)
    {
        Name = name;
        Price = price;
        InStock = inStock;
        PDTypeID = pDTypeID;
    }
}
