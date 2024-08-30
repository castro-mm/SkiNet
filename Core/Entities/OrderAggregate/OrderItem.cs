using Core.Base;

namespace Core.Entities.OrderAggregate;

public class OrderItem : Entity
{   
    public ProductItemOrdered ItemOrdered { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
