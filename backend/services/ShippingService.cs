using backend.Data;
using backend.interfaces;
using backend.models.models;
using Microsoft.EntityFrameworkCore;

namespace backend.services
{
    public class ShippingService : IShippingService
    {
        private readonly DataContext context;

        public ShippingService(DataContext context) =>
            this.context = context;

        public Task deleteRequest(Guid? id, int? intId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Shipping>> getResponse() =>
            context.Shippings.ToListAsync();

        public Task<List<Shipping>> getResponse(Guid? id, int? intId)
        {
            throw new NotImplementedException();
        }

        public Task<Shipping?> getSingleResponse(Guid? id, int? intId)
        {
            throw new NotImplementedException();
        }

        public Task postRequest(Shipping t)
        {
            throw new NotImplementedException();
        }

        public Task putRequest(Shipping t, Guid? id, int? intId)
        {
            throw new NotImplementedException();
        }
    }
}