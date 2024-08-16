using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductRepository productRepository) : ControllerBase
    {
        private readonly IProductRepository _productRepository = productRepository;

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
        {
            return Ok(await this._productRepository.GetProductsAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id) 
        {
            var product = await this._productRepository.GetProductByIdAsync(id);

            if (product == null) 
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            this._productRepository.AddProduct(product);

            if(await this._productRepository.SaveChangesAsync())
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            else 
                return BadRequest("Problem creating product");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (product.Id != id || ! this._productRepository.ProductExists(id))
                return BadRequest("Cannot update this product.");

            this._productRepository.UpdateProduct(product);

            if(await this._productRepository.SaveChangesAsync())
                return NoContent();
            else 
                return BadRequest("Problem updating product");

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await this._productRepository.GetProductByIdAsync(id);

            if (product == null) 
                return NotFound();

            this._productRepository.DeleteProduct(product);

            if(await this._productRepository.SaveChangesAsync())
                return NoContent();
            else 
                return BadRequest("Problem deleting product");
        }
    }
}
