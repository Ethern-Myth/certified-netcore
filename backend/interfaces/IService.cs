using System.Reflection.Metadata;
using System;
namespace backend.interfaces;

public interface IService<T>
{
    public Task<List<T>> getResponse();
    public Task<List<T>> getResponse(Guid? id, int? intId);
    public Task<T?> getSingleResponse(Guid? id, int? intId);
    public Task postRequest(T t);
    public Task putRequest(T t, Guid? id, int? intId);
    public Task deleteRequest(Guid? id, int? intId);
}