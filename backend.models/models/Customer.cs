using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.models.models;

namespace backend.models;

[Table("customer")]
public class Customer : BaseModel
{
    [Key]
    public Guid CustomerID { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; } = "default";
    [Required]
    public bool Status { get; set; } = true;
    [ForeignKey(nameof(Roles))]
    public int RoleID { get; set; }
    public virtual Roles? Roles { get; set; }
    public string? Phone { get; set; }
    public Customer() { }

    public Customer
       (
       Guid customerID,
       string name,
       string surname,
       string email,
       string password,
       bool status,
       int roleID,
       string? phone)
    {
        CustomerID = customerID;
        Name = name;
        Surname = surname;
        Email = email;
        Password = password;
        Status = status;
        RoleID = roleID;
        Phone = phone;
    }

    public Customer
       (
       string name,
       string surname,
       string email,
       string password,
       bool status,
       int roleID,
       string? phone)
    {
        Name = name;
        Surname = surname;
        Email = email;
        Password = password;
        Status = status;
        RoleID = roleID;
        Phone = phone;
    }

    public Customer(
        Guid id,
        string name,
        string surname,
        string email,
        string? phone)
    {
        CustomerID = id;
        Name = name;
        Surname = surname;
        Email = email;
        Phone = phone;
    }

    public Customer(
        string name,
        string surname,
        string email,
        string? phone)
    {
        Name = name;
        Surname = surname;
        Email = email;
        Phone = phone;
    }

}
