using Core.Entities.Identity;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers.Extensions;

public static class SeedDataExtensions
{
    public static async Task SeedData(this WebApplication app)
    {
        try
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<StoreContext>();
            await context.Database.MigrateAsync();

            var userManager = services.GetRequiredService<UserManager<AppUser>>();

            await StoreContextSeed.SeedAsync(context, userManager);            
        }
        catch (Exception ex)
        {            
            Console.WriteLine(ex);
            throw;
        }
    }
}
