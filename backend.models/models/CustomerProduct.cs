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
    public virtual ICollection<Product> Products { get; set; }
    [Required]
    public double Subtotal
    {
        get
        {
            double subtotal = 0.00;
            foreach (var item in Products)
            {
                subtotal += item.Price;
            }
            return subtotal;
        }
        set { }
    }
    public CustomerProduct() { }

    public CustomerProduct(
        Guid cPID,
        Guid customerID,
        Customer customer,
        ICollection<Product> products,
        double subTotal)
    {
        CPID = cPID;
        CustomerID = customerID;
        Customer = customer;
        Products = products;
        Subtotal = subTotal;
    }
    public CustomerProduct(
        Guid customerID,
        ICollection<Product> products)
    {
        CustomerID = customerID;
        Products = products;
    }

    public CustomerProduct(
        ICollection<Product> products,
        double subTotal
    )
    {
        Products = products;
        Subtotal = subTotal;
    }
}
