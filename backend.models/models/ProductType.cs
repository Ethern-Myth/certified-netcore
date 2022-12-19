using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace backend.models.models;

[Table("product_type")]
public class ProductType
{
    [Key]
    public int PDTypeID { get; set; }
    [Required]
    public string Category { get; set; }

    public ProductType() { }

    public ProductType(int pdTypeID, string category)
    {
        PDTypeID = pdTypeID;
        Category = category;
    }

}
