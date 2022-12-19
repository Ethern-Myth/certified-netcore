using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.models.models;

[Table("roles")]
public class Roles
{
    [Key]
    public int RoleID { get; set; }
    [Required]
    public string Name { get; set; }
    public Roles() { }

    public Roles(int roleID, string name)
    {
        RoleID = roleID;
        Name = name;
    }
}
