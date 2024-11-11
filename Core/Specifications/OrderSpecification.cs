using Core.Entities.OrderAggregate;

namespace Core.Specifications;

public class OrderSpecification : Specification<Order>
{
    public OrderSpecification(string email) : base(x => x.BuyerEmail == email)
    {    
        AddInclude(x => x.OrderItems);
        AddInclude(x => x.DeliveryMethod);
        AddOrderByDescending(x => x.OrderDate);    
    }
    public OrderSpecification(int id) : base(x => x.Id == id)
    {
        AddInclude("OrderItems");
        AddInclude("DeliveryMethod");
    }

    public OrderSpecification(string email, int id) :  base(x => x.BuyerEmail == email && x.Id == id)
    {        
        AddInclude("OrderItems");
        AddInclude("DeliveryMethod");
    }

    public OrderSpecification(string paymentIntentId, bool isPaymentIntent = true) : base (x => x.PaymentIntentId == paymentIntentId)
    {
        AddInclude("OrderItems");
        AddInclude("DeliveryMethod");        
    }

    public OrderSpecification(OrderSpecParams specParams) : base(x => string.IsNullOrEmpty(specParams.Status) || x.Status == ParseStatus(specParams.Status))
    {
        AddInclude("OrderItems");
        AddInclude("DeliveryMethod");
        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        AddOrderByDescending(x => x.OrderDate);
    }

    private static OrderStatus? ParseStatus(string status)
    {
        if (Enum.TryParse<OrderStatus>(status, true, out var result)) return result;
        return null;
    }
}
