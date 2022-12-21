using backend.Data;
using backend.interfaces;
using backend.models.models;
using Microsoft.EntityFrameworkCore;

namespace backend.services;

public class CountryService : ICountryService
{
    private readonly DataContext context;

    public CountryService(DataContext context) =>
        this.context = context;

    public Task<List<Country>> getResponse() =>
        context.Countries.ToListAsync();

    public Task deleteRequest(Guid? id, int? intId) =>
        throw new NotImplementedException();
    public Task<List<Country>> getResponse(Guid? id, int? intId) =>
        throw new NotImplementedException();

    public Task<Country?> getSingleResponse(Guid? id, int? intId) =>
        throw new NotImplementedException();

    public Task postRequest(Country t) =>
        throw new NotImplementedException();

    public Task putRequest(Country t, Guid? id, int? intId) =>
        throw new NotImplementedException();
}
