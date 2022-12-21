using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.models.models;

[Table("customer_product")]
public class CustomerProduct : BaseModel
{
    [Key]
    public Guid CPID { get; set; }

    [ForeignKey(nameof(Customer))]
    public Guid CustomerID { get; set; }
    public virtual Customer Customer { get; set; }
    [NotMapped]
    public virtual Product Product { get; set; }
    public virtual ICollection<ProductQuantity> Products { get; set; }
    [Required]
    public double Subtotal
    {
        get
        {
            double subtotal = 0.00;
            foreach (var item in Products)
            {
                subtotal += item.ProductTotal;

            }
            return subtotal;
        }
        set { }
    }
    public CustomerProduct(
        Guid customerID,
        ICollection<ProductQuantity> products)
    {
        CustomerID = customerID;
        Products = products;
    }
}
