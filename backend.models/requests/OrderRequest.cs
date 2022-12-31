using System.ComponentModel.DataAnnotations;
namespace backend.models.requests;
public struct OrderRequest
{
    [Required(ErrorMessage = "CCID is required")]
    public Guid CCID { get; set; }
    [Required(ErrorMessage = "Is paid is required")]
    public bool IsPaid { get; set; }
}
