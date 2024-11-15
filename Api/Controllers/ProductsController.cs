using Api.Controllers.Base;
using Api.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ProductsController(IUnitOfWork unitOfWork) : ApiController
    {
        [Cache(600)]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery] ProductSpecParams specParams)
        {
            var spec = new ProductSpecification(specParams);

            return await base.CreatePagedResult(unitOfWork.Repository<Product>(), spec, specParams.PageIndex, specParams.PageSize);
        }

        [Cache(600)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id) 
        {
            var product = await unitOfWork.Repository<Product>().GetByIdAsync(id);

            if (product == null) 
                return NotFound();

            return Ok(product);
        }

        [InvalidateCache("api/products|")]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            unitOfWork.Repository<Product>().Add(product);

            if(await unitOfWork.Complete())
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            else 
                return BadRequest("Problem creating product");
        }

        [InvalidateCache("api/products|")]
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (product.Id != id || !unitOfWork.Repository<Product>().Exists(id))
                return BadRequest("Cannot update this product.");

            unitOfWork.Repository<Product>().Update(product);

            if(await unitOfWork.Complete())
                return NoContent();
            else 
                return BadRequest("Problem updating product");

        }

        [InvalidateCache("api/products|")]
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await unitOfWork.Repository<Product>().GetByIdAsync(id);

            if (product == null) 
                return NotFound();

            unitOfWork.Repository<Product>().Remove(product);

            if(await unitOfWork.Complete())
                return NoContent();
            else 
                return BadRequest("Problem deleting product");
        }

        [Cache(10000)]
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            var spec = new BrandListSpecification();

            return Ok(await unitOfWork.Repository<Product>().ListAsync(spec));
        }

        [Cache(10000)]
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            var spec = new TypeListSpecification();

            return Ok(await unitOfWork.Repository<Product>().ListAsync(spec));
        }
    }
}
