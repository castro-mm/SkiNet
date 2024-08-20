using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;

namespace Api.Controllers.Extensions;

public static class MapperServicesExtensions
{
    public static void AddMappingClassesServices(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

    }
}
