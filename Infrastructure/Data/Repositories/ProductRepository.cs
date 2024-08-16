using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class ProductRepository(StoreContext context) : IProductRepository
{
    private readonly StoreContext _context = context;

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return await this._context.Products.ToListAsync();
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

}

