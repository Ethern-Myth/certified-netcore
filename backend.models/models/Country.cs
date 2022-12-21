using System.ComponentModel.DataAnnotations;

namespace backend.models.models;

public class Country
{
    [Key]
    public string Name { get; set; }
    public string? Code { get; set; }
}
