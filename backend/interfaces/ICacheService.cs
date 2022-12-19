namespace backend.interfaces;

public interface ICacheService
{
    T GetData<T>(string key);
    bool SetData<T>(string key, T value, DateTimeOffset expirationTime);
    object RemoveDate(string key);
}
