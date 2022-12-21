using backend.Data;
using backend.interfaces;
using backend.models.models;
using Microsoft.EntityFrameworkCore;

namespace backend.services;

public class OrderService : IOrderService
{
    private readonly DataContext context;

    public OrderService(DataContext context) =>
        this.context = context;

    public async Task deleteRequest(Guid? id, int? intId)
    {
        var response = context.Orders.Single(o => o.OrderID == id);
        context.ChangeTracker.Clear();
        context.Remove(response);
        await context.SaveChangesAsync();
    }

    public Task<List<Order>> getResponse() =>
        context.Orders.AsNoTracking().ToListAsync();

    public Task<List<Order>> getResponse(Guid? id, int? intId) =>
        context.Orders
        .Where(o => o.OrderID == id
        || o.CCID == id)
        .AsNoTracking()
        .ToListAsync();

    public Task<CustomerCollection> getCollection(Guid id) =>
        context.CustomerCollections
        .AsNoTracking()
        .FirstAsync(cc => cc.CCID == id);

    public async Task postRequest(Order t)
    {
        context.ChangeTracker.Clear();
        await context.AddAsync(t);
        await context.SaveChangesAsync();
    }

    public async Task putRequest(Order t, Guid? id, int? intId)
    {
        context.ChangeTracker.Clear();
        if (context.Orders.Any(o => o.OrderID == id))
        {
            await Task.Run(() => context.Update(t));
        }
        await context.SaveChangesAsync();
    }
    public Task<Order?> getSingleResponse(Guid? id, int? intId) =>
       throw new NotImplementedException();


}
