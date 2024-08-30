using Api.DTOs;
using Core.Entities.OrderAggregate;

namespace Api.Controllers.Extensions;

public static class OrderMappingExtensions
{
    public static OrderDto ToDto(this Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            BuyerEmail = order.BuyerEmail,
            OrderDate = order.OrderDate,
            ShippingAddress = order.ShippingAddress,
            PaymentSummary = order.PaymentSummary,
            DeliveryMethod = order.DeliveryMethod.Description,
            ShippingPrice = order.DeliveryMethod.Price,
            OrderItems = order.OrderItems.Select(x => x.ToDto()).ToList(),
            Subtotal = order.Subtotal,
            Status = order.Status.ToString(),
            Total = order.Total,
            PaymentIntentId = order.PaymentIntentId
        };
    }

    public static OrderItemDto ToDto(this OrderItem item)
    {
        return new OrderItemDto 
        {
            ProductId = item.ItemOrdered.ProductId,
            ProductName = item.ItemOrdered.Name,
            PictureUrl = item.ItemOrdered.PictureUrl,
            Price = item.Price,
            Quantity = item.Quantity
        };
    }
}
