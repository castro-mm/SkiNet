using Core.Interfaces;
using Infrastructure.Services;
using StackExchange.Redis;

namespace Api.Controllers.Extensions;

public static class RedisServicesExtensions
{
    public static void AddRedisServicesExtensions(this IServiceCollection services, ConfigurationManager configuration)
    {
        // Redis
        services.AddSingleton<IConnectionMultiplexer>(config =>         
        {
            var connectionString = configuration.GetConnectionString("Redis") ?? throw new Exception("Cannot get redis connection string");
            var configurationOptions = ConfigurationOptions.Parse(connectionString, true);

            return ConnectionMultiplexer.Connect(configurationOptions);
        });
        services.AddSingleton<ICartService, CartService>();

    }
}
