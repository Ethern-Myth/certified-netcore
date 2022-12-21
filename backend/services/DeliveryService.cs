using backend.Data;
using backend.interfaces;
using backend.models.models;
using Microsoft.EntityFrameworkCore;

namespace backend.services;

public class DeliveryService : IDeliveryService
{
    private readonly DataContext context;

    public DeliveryService(DataContext context) =>
        this.context = context;

    public async Task deleteRequest(Guid? id, int? intId)
    {
        var response = context.Deliveries.Single(d => d.OrderID == id);
        context.ChangeTracker.Clear();
        context.Remove(response);
        await context.SaveChangesAsync();
    }

    public Task<List<Delivery>> getResponse() =>
        context.Deliveries.ToListAsync();

    public Task<Delivery?> getSingleResponse(Guid? id, int? intId) =>
        context.Deliveries.FirstOrDefaultAsync(d => d.OrderID == id);

    public async Task postRequest(Delivery t)
    {
        context.ChangeTracker.Clear();
        await context.AddAsync(t);
        await context.SaveChangesAsync();
    }

    public async Task putRequest(Delivery t, Guid? id, int? intId)
    {
        context.ChangeTracker.Clear();
        if (context.Deliveries.Any(d => d.OrderID == id))
        {
            await Task.Run(() => context.Update(t));
        }
        await context.SaveChangesAsync();
    }
    public Task<List<Delivery>> getResponse(Guid? id, int? intId) =>
        throw new NotImplementedException();

}
