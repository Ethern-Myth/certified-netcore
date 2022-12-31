using backend.Data;
using backend.models;
using backend.interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.services;
public class CustomerService : ICustomerService
{
    private readonly DataContext context;
    public CustomerService(DataContext context) => this.context = context;
    public async Task deleteRequest(Guid? id, int? intId)
    {
        var response = context.Customers.Single(c => c.CustomerID == id);
        context.ChangeTracker.Clear();
        context.Remove(response);
        await context.SaveChangesAsync();
    }

    public Task<Customer?> GetCurrentUser(
        string email,
        string password) =>
        context.Customers
        .Where(c => c.RoleID == c.Roles.RoleID)
        .Include(c => c.Roles)
        .FirstOrDefaultAsync(
            predicate: c =>
            c.Email == email && c.Password == password);

    public Task<List<Customer>> getResponse() =>
        context.Customers
        .Where(c => c.RoleID == c.Roles.RoleID)
        .Include(c => c.Roles)
        .ToListAsync();

    public Task<Customer?> getSingleResponse(Guid? id, int? intId) =>
        context.Customers
        .Where(c => c.RoleID == c.Roles.RoleID)
        .Include(c => c.Roles)
        .FirstOrDefaultAsync(c => c.CustomerID == id);

    public Task<bool> IsDuplicate(string email) =>
        context.Customers
        .AnyAsync(c => c.Email == email);

    public async Task postRequest(Customer t)
    {
        context.ChangeTracker.Clear();
        await context.AddAsync(t);
        await context.SaveChangesAsync();
    }

    public async Task putRequest(Customer t, Guid? id, int? intId)
    {
        context.ChangeTracker.Clear();
        if (context.Customers.Any(c => c.CustomerID == id))
        {
            await Task.Run(() => context.Update(t));
        }
        await context.SaveChangesAsync();
    }
    public Task<List<Customer>> getResponse(Guid? id, int? intId) =>
       throw new NotImplementedException();

    public Task<List<Customer?>> getSingleListResponse(Guid? id, int? intId)
    {
        throw new NotImplementedException();
    }

}
