using Api.Controllers.Base;
using Core.Entities.Cart;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class CartController(ICartService cartService) : ApiController
{
    [HttpGet]
    public async Task<ActionResult<ShoppingCart>> GetCartById(string id)
    {
        var cart = await cartService.GetCartAsync(id);

        return Ok(cart ?? new ShoppingCart { Id = id });
    }

    [HttpPost]
    public async Task<ActionResult<ShoppingCart>> UpdateCart([FromBody] ShoppingCart cart)
    {
        var updatedCart = await cartService.SetCartAsync(cart);

        if (updatedCart == null) return BadRequest("Problem with the cart");

        return Ok(updatedCart);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteCart(string id)
    {
        var isDeletedCart = await cartService.DeleteCartAsync(id);

        if (!isDeletedCart) return BadRequest("Problem in deleting the cart");

        return Ok(); 
    }
}
