using System.ComponentModel.DataAnnotations;

namespace backend.models.requests;

public struct CustomerCollectionRequest
{
    [Required]
    public Guid CPID { get; set; }
    [Required]
    public Guid CustomerID { get; set; }
}
