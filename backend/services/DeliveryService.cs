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

    public Task deleteRequest(Guid? id, int? intId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Delivery>> getResponse() =>
        context.Deliveries.ToListAsync();

    public Task<List<Delivery>> getResponse(Guid? id, int? intId)
    {
        throw new NotImplementedException();
    }

    public Task<Delivery?> getSingleResponse(Guid? id, int? intId)
    {
        throw new NotImplementedException();
    }

    public Task postRequest(Delivery t)
    {
        throw new NotImplementedException();
    }

    public Task putRequest(Delivery t, Guid? id, int? intId)
    {
        throw new NotImplementedException();
    }
}
