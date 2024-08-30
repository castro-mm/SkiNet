using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.Services;

namespace Api.Controllers.Extensions;

public static class MapperServicesExtensions
{
    public static void AddMappingClassesServices(this IServiceCollection services)
    {
        services
            .AddScoped<IProductRepository, ProductRepository>()
            .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>))
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IPaymentService, PaymentService>();     
    }
}
