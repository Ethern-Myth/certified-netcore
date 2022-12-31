using System.ComponentModel.DataAnnotations;

namespace backend.models.requests;

public struct CustomerCollectionRequest
{
    [Required(ErrorMessage = "CPID is required")]
    public Guid CPID { get; set; }
    [Required(ErrorMessage = "Customer Id is required")]
    public Guid CustomerID { get; set; }
}
