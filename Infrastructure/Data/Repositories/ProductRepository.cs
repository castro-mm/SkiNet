using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class ProductRepository(StoreContext context) : IProductRepository
{
    private readonly StoreContext _context = context;

    public async Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort)
    {
        var query = this._context.Products
            .AsQueryable()
            .Where(x => x.Brand == brand || brand == null || brand == "")
            .Where(x => x.Type == type || type == null || type == "");

        if (!string.IsNullOrEmpty(sort))
        {
            query = sort switch
            {
                "priceAsc" => query.OrderBy(x => x.Price),
                "priceDesc" => query.OrderByDescending(x => x.Price),
                _ => query.OrderBy(x => x.Name)
            };
        }

        return await query.ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await this._context.Products.FindAsync(id);
    }

    public void AddProduct(Product product)
    {
        this._context.Products.Add(product);
    }

    public void UpdateProduct(Product product)
    {
        this._context.Entry(product).State = EntityState.Modified;
    }

    public void DeleteProduct(Product product)
    {
        this._context.Products.Remove(product);
    }

    public bool ProductExists(int id)
    {
        return this._context.Products.Any(x => x.Id == id);
    }
    public async Task<bool> SaveChangesAsync()
    {
        return await this._context.SaveChangesAsync() > 0;
    }

    public async Task<IReadOnlyList<string>> GetBrandsAsync()
    {
        return await this._context.Products
            .Select(x => x.Brand)
            .Distinct()
            .ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetTypesAsync()
    {
        return await this._context.Products
            .Select(x => x.Type)
            .Distinct()
            .ToListAsync();
    }
}

