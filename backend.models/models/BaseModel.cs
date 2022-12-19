using System.ComponentModel.DataAnnotations.Schema;

namespace backend.models.models;

public class BaseModel
{
    public DateTimeOffset? DateAdded { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? DateUpdated { get; set; } = DateTimeOffset.UtcNow;
}