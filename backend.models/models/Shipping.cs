using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.models.models;

[Table("shipping")]
public class Shipping
{
    [Key]
    public int ShippingID { get; set; }
    [Required]
    public string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    [Required]
    public string Suburb { get; set; }
    [Required]
    public string Town { get; set; }
    [Required]
    public string Region { get; set; }
    [Required]
    public int PostalCode { get; set; }
    [ForeignKey(nameof(Country))]
    public string Name { get; set; }
    public virtual Country Country { get; set; }
    public Shipping(
        string addressLine1,
        string? addressLine2,
        string suburb,
        string town,
        string region,
        int postalCode,
        string name)
    {
        AddressLine1 = addressLine1;
        AddressLine2 = addressLine2;
        Suburb = suburb;
        Town = town;
        Region = region;
        PostalCode = postalCode;
        Name = name;
    }
}
