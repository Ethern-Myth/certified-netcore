using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.models.models;

[Table("customer_collection")]
public class CustomerCollection : BaseModel
{
    [Key]
    public Guid CCID { get; set; }

    [ForeignKey(nameof(CustomerProduct))]
    public Guid CPID { get; set; }
    public virtual CustomerProduct CustomerProduct { get; set; }

    [ForeignKey(nameof(Customer))]
    public Guid CustomerID { get; set; }
    public virtual Customer Customer { get; set; }

    public CustomerCollection(
        Guid cCID,
        Guid customerID,
        Customer customer,
        Guid cPID,
        CustomerProduct customerProduct)
    {
        CCID = cCID;
        CustomerID = customerID;
        Customer = customer;
        CPID = cPID;
        CustomerProduct = customerProduct;
    }

    public CustomerCollection(
        Guid cPID,
        Guid customerID)
    {
        CustomerID = customerID;
        CPID = cPID;
    }

}
