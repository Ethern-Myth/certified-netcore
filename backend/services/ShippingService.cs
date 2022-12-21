using backend.Data;
using backend.interfaces;
using backend.models;
using backend.models.models;
using Microsoft.EntityFrameworkCore;

namespace backend.services;

public class ShippingService : IShippingService
{
    private readonly DataContext context;
    public ShippingService(DataContext context) =>
        this.context = context;

    public async Task deleteRequest(Guid? id, int? intId)
    {
        var response = context.Shippings.Single(s => s.ShippingID == intId);
        context.ChangeTracker.Clear();
        context.Remove(response);
        await context.SaveChangesAsync();
    }

    public Task<List<Shipping>> getResponse() =>
        context.Shippings.ToListAsync();

    public Task<Shipping?> getSingleResponse(Guid? id, int? intId) =>
        context.Shippings
        .FirstOrDefaultAsync(s => s.ShippingID == intId);

    public Task<Customer> getCustomer(Guid id) =>
        context.Customers
            .Where(c => c.RoleID == c.Roles.RoleID)
            .Include(c => c.Roles)
            .FirstOrDefaultAsync(c => c.CustomerID == id);

    public Task<Country> getCountry(string name) =>
        context.Countries
        .FirstOrDefaultAsync(c => c.Name == name);

    public async Task postRequest(Shipping t)
    {
        context.ChangeTracker.Clear();
        await context.AddAsync(t);
        await context.SaveChangesAsync();
    }

    public async Task putRequest(Shipping t, Guid? id, int? intId)
    {
        context.ChangeTracker.Clear();
        if (context.Shippings.Any(s => s.ShippingID == intId))
            await Task.Run(() => context.Update(t));
        await context.SaveChangesAsync();
    }

    public Task<List<Shipping>> getResponse(Guid? id, int? intId) =>
        throw new NotImplementedException();


}
