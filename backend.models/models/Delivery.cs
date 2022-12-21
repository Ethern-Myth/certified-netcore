using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.models.models;

[Table("delivery")]
public class Delivery
{
    [Key]
    [ForeignKey(nameof(Order))]
    public Guid OrderID { get; set; }
    public virtual Order Order { get; set; }
    [Required]
    public bool IsDelivered { get; set; }
}