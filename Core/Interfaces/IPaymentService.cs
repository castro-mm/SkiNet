using Core.Entities.Cart;

namespace Core.Interfaces;

public interface IPaymentService
{
    Task<ShoppingCart> CreateOrUpdatePaymentIntent(string cartId);
}