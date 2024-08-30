using Api.Controllers.Base;
using Api.Controllers.Extensions;
using Api.DTOs;
using Api.Extensions;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
public class OrdersController(ICartService cartService, IUnitOfWork unitOfWork) : ApiController
{
    [HttpPost]
    public async Task<ActionResult<OrderDto>> CreateOrder(CreateOrderDto orderDto) 
    {
        var email = User.GetEmail();
        
        var cart = await cartService.GetCartAsync(orderDto.CartId);
        if (cart == null) return BadRequest("Cart not found");
        if (cart.PaymentIntentId == null) return BadRequest("No payment intent for this order");

        var items = new List<OrderItem>();
        foreach (var item in cart.Items)
        {
            var productItem = await unitOfWork.Repository<Product>().GetByIdAsync(item.ProductId);
            if (productItem == null) return BadRequest("Problem with the order");

            var itemOrdered = new ProductItemOrdered 
            {
                ProductId = item.ProductId,
                Name = item.ProductName,
                PictureUrl = item.PictureUrl
            };

            var orderItem = new OrderItem
            {
                ItemOrdered = itemOrdered,
                Price = productItem.Price,
                Quantity = item.Quantity
            };
            items.Add(orderItem);
        }

        var deliveryMethod = await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(orderDto.DeliveryMethodId);
        if (deliveryMethod == null) return BadRequest("No delivery method selected");

        var order = new Order
        {
            OrderItems = items,
            DeliveryMethod = deliveryMethod,
            ShippingAddress = orderDto.ShippingAddress,
            Subtotal = items.Sum(x => x.Price * x.Quantity),
            PaymentSummary = orderDto.PaymentSummary,
            PaymentIntentId = cart.PaymentIntentId,
            BuyerEmail = email
        };

        unitOfWork.Repository<Order>().Add(order);

        if (!await unitOfWork.Complete()) return BadRequest("Problem creating order");

        return Ok(order.ToDto());
    }
    
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrderForUser()
    {
        var spec = new OrderSpecification(User.GetEmail());

        var orders = await unitOfWork.Repository<Order>().ListAsync(spec);

        var orderToReturn = orders.Select(x => x.ToDto()).ToList();

        return Ok(orderToReturn);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Order>> GetOrderById(int id)
    {
        var spec = new OrderSpecification(User.GetEmail(), id);

        var order = await unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

        if (order == null) return BadRequest("Problem to get the order");
        
        return Ok(order.ToDto());
    }
}
