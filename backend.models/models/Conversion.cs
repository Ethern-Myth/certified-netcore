using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.models.models;
[Table("conversion")]
public class Conversion
{
    [Key]
    public int ConversionID { get; set; }
    [Required]
    public string Unit { get; set; }

    public Conversion() { }

    public Conversion(int conversionID, string unit)
    {
        ConversionID = conversionID;
        Unit = unit;
    }
}
