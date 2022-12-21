using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.models.models;

[Table("order")]
public class Order : BaseModel
{
    [Key]
    public Guid OrderID { get; set; }
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
    [Required]
    public int ItemCount { get; set; }
    [Required]
    public double OrderTotal { get; set; }
    [Required]
    public bool IsPaid { get; set; } = false;

    [ForeignKey(nameof(CustomerCollection))]
    public Guid CCID { get; set; }
    public virtual CustomerCollection CustomerCollection { get; set; }

    [ForeignKey(nameof(Shipping))]
    public int ShippingID { get; set; }
    public virtual Shipping Shipping { get; set; }
    public Order(
        Guid cCID,
        bool isPaid)
    {
        CCID = cCID;
        IsPaid = isPaid;
    }
}
