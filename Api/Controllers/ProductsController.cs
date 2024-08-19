using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IGenericRepository<Product> repository) : ControllerBase
    {
        private readonly IGenericRepository<Product> _repository = repository;

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type, string? sort)
        {
            var spec = new ProductSpecification(brand, type, sort);

            var products = await this._repository.ListAsync(spec);

            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id) 
        {
            var product = await this._repository.GetByIdAsync(id);

            if (product == null) 
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            this._repository.Add(product);

            if(await this._repository.SaveAllAsync())
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            else 
                return BadRequest("Problem creating product");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (product.Id != id || ! this._repository.Exists(id))
                return BadRequest("Cannot update this product.");

            this._repository.Update(product);

            if(await this._repository.SaveAllAsync())
                return NoContent();
            else 
                return BadRequest("Problem updating product");

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await this._repository.GetByIdAsync(id);

            if (product == null) 
                return NotFound();

            this._repository.Remove(product);

            if(await this._repository.SaveAllAsync())
                return NoContent();
            else 
                return BadRequest("Problem deleting product");
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            var spec = new BrandListSpecification();

            return Ok(await this._repository.ListAsync(spec));
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            var spec = new TypeListSpecification();

            return Ok(await this._repository.ListAsync(spec));
        }
    }
}
