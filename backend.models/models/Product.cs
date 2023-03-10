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
    public string? Desc { get; set; }
    [Required]
    public string Brand { get; set; }
    [Required]
    public double Price { get; set; } = 0.00;
    [Required]
    public bool InStock { get; set; } = true;

    [ForeignKey(nameof(ProductType))]
    public int PDTypeID { get; set; }
    public virtual ProductType? ProductType { get; set; }
    public double? ConversionSize { get; set; }

    [ForeignKey(nameof(Conversion))]
    public int ConversionID { get; set; }
    public virtual Conversion? Conversion { get; set; }
    public string? ImageName { get; set; }
    public string? ImageUrl { get; set; }
    public Product(
        string name,
        string? desc,
        string brand,
        double price,
        bool inStock,
        int pDTypeID,
        double? conversionSize,
        int conversionID,
        string imageName,
        string? imageUrl)
    {
        Name = name;
        Desc = desc;
        Brand = brand;
        Price = price;
        InStock = inStock;
        PDTypeID = pDTypeID;
        ConversionSize = conversionSize;
        ConversionID = conversionID;
        ImageName = imageName;
        ImageUrl = imageUrl;
    }
}
