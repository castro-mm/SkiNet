using System.Reflection;
using System.Text.Json;
using Core.Base;
using Core.Entities;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context, UserManager<AppUser> userManager)
    {
        if(!userManager.Users.Any(x => x.UserName == "admin@test.com"))
        {
            var user = new AppUser {
                UserName = "admin@test.com",
                Email = "admin@test.com"
            };

            await userManager.CreateAsync(user, "Pa$$w0rd");
            await userManager.AddToRoleAsync(user, "Admin");
        }

        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        await Seed<Product>(context, path + @"/Data/SeedData/products.json");
        await Seed<DeliveryMethod>(context, path + @"/Data/SeedData/delivery.json");
    }

    private static async Task Seed<T>(StoreContext context, string filePath) where T : Entity 
    {
        if (!context.Set<T>().Any())
        {
            var fileData = await File.ReadAllTextAsync(filePath);

            var data = JsonSerializer.Deserialize<List<T>>(fileData);
            if (data == null) return;
            context.Set<T>().AddRange(data);

            await context.SaveChangesAsync();
        }
    }
}
