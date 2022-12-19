using System.ComponentModel.DataAnnotations;

namespace backend.models.requests;
public struct OrderRequest
{
    [Required]
    public Guid CCID { get; set; }
    [Required]
    public bool IsPaid { get; set; }
    public OrderRequest()
    {
        IsPaid = false;
    }
}
