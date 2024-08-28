using System.Text.Json;
using Core.Base;
using Core.Entities;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context)
    {
        await Seed<Product>(context, "../Infrastructure/Data/SeedData/products.json");
        await Seed<DeliveryMethod>(context, "../Infrastructure/Data/SeedData/delivery.json");
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
