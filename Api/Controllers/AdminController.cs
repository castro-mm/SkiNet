using Api.Controllers.Base;
using Api.Controllers.Extensions;
using Api.DTOs;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController(IUnitOfWork unitOfWork, IPaymentService paymentService) : ApiController
{
    [HttpGet("orders")]
    public async Task<ActionResult<IReadOnlyList<OrderDto>>> GerOrders([FromQuery] OrderSpecParams specParams)
    {
        var spec = new OrderSpecification(specParams);

        return await CreatePagedResult(unitOfWork.Repository<Order>(), spec, specParams.PageIndex, specParams.PageSize, x => x.ToDto());
    }

    [HttpGet("orders/{id:int}")]
    public async Task<ActionResult<OrderDto>> GetOrderById(int id)
    {
        var spec = new OrderSpecification(id);

        var order = await unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

        if (order == null) return BadRequest("No order with that id");

        return Ok(order.ToDto());
    }

    [HttpPost("orders/refund/{id:int}")]
    public async Task<ActionResult<OrderDto>> RefundOrder(int id)
    {
        var spec = new OrderSpecification(id);
        
        var order = await unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

        if (order == null) return BadRequest("No order with that id");
        if (order.Status == OrderStatus.Pending) return BadRequest("Payment not received for this order");

        var result = await paymentService.RefundPayment(order.PaymentIntentId);

        if (result == "succeeded")
        {
            order.Status = OrderStatus.Refunded; 
        
            await unitOfWork.Complete();

            return Ok(order.ToDto());
        }

        return BadRequest("Problem refunding order");
    }
}
