namespace backend.Data;

public class RedisContext
{
    private IConfiguration _configuration;
    public RedisContext(IConfiguration configuration) =>
        _configuration = configuration;

    public string RedisURL
    {
        get
        {
            return _configuration.GetConnectionString("RedisURL");
        }
    }
}
