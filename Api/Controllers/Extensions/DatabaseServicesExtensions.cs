using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers.Extensions;

public static class DatabaseServicesExtensions
{
    public static void AddDatabaseServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<StoreContext>(opt => {
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

    }
}
