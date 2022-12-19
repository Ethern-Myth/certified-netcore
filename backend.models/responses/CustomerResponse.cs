using backend.models.models;

namespace backend.models.responses;

public record CustomerResponse
{
    public string Name { get; }
    public string Surname { get; }
    public string Email { get; }
    public string? Phone { get; }

    public CustomerResponse(
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

public record FullCustomerResponse
{
    public Guid CustomerID { get; }
    public string Name { get; }
    public string Surname { get; }
    public string Email { get; }
    public string? Phone { get; }
    public Roles? Roles { get; }
    public bool Status { get; }
    public FullCustomerResponse() { }

    public FullCustomerResponse(
        Guid customerID,
        string name,
        string surname,
        string email,
        string? phone,
        Roles? roles,
        bool status)
    {
        CustomerID = customerID;
        Name = name;
        Surname = surname;
        Email = email;
        Phone = phone;
        Roles = roles;
        Status = status;
    }
}

public record AdminCustomerResponse
{
    public Guid CustomerID { get; }
    public string Name { get; }
    public string Surname { get; }
    public string Email { get; }
    public string Password { get; }
    public string? Phone { get; }
    public Roles? Roles { get; }
    public bool Status { get; }
    public AdminCustomerResponse() { }

    public AdminCustomerResponse(
        Guid customerID,
        string name,
        string surname,
        string email,
        string password,
        string? phone,
        Roles? roles,
        bool status)
    {
        CustomerID = customerID;
        Name = name;
        Surname = surname;
        Email = email;
        Password = password;
        Phone = phone;
        Roles = roles;
        Status = status;
    }
}

public record PartialCustomerResponse
{
    public Guid CustomerID;
    public string Name;
    public string Surname;
    public string Email;
    public string? Phone;
}
