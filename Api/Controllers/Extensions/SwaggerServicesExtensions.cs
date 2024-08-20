namespace Api.Controllers.Extensions;

public static class SwaggerExtensions
{
    public static void AddSwaggerServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public static void AddSwaggerAppServices(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
