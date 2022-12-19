using backend.models;
using backend.models.models;

namespace backend.interfaces;

public interface ICustomerService : IService<Customer>
{
    public Task<Customer?> GetCurrentUser(string email, string password);
    public Task<Roles?> GetRoles(int id);
    public Task<bool> IsDuplicate(string email);
}
