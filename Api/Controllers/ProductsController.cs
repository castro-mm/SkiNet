using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(StoreContext context) : ControllerBase
    {
        private readonly StoreContext _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Ok(await this._context.Products.ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id) 
        {
            var product = await this._context.Products.FindAsync(id);

            if (product == null) 
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            this._context.Products.Add(product);

            await this._context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (product.Id != id || !ProductExists(id))
                return BadRequest("Cannot update this product.");

            this._context.Entry(product).State = EntityState.Modified;

            await this._context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await this._context.Products.FindAsync(id);

            if (product == null) 
                return NotFound();

            this._context.Products.Remove(product);

            await this._context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return this._context.Products.Any(p => p.Id == id);
        }
    }
}
