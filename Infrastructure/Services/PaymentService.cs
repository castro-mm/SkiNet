using Core.Entities;
using Core.Entities.Cart;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace Infrastructure.Services;

public class PaymentService : IPaymentService
{
    private readonly ICartService cartService;
    private readonly IUnitOfWork unitOfWork;
    public PaymentService(IConfiguration config, ICartService cartService, IUnitOfWork unitOfWork)
    {
        StripeConfiguration.ApiKey = config["StripeSettings:SecretKey"];
        this.unitOfWork = unitOfWork;
        this.cartService = cartService;
    }

    public async Task<ShoppingCart> CreateOrUpdatePaymentIntent(string cartId)
    {

        var cart = await cartService.GetCartAsync(cartId);
        if (cart == null) return null!;

        var shippingPrice = 0m;

        if (cart.DeliveryMethodId.HasValue)
        {
            var deliveryMethod = await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(cart.DeliveryMethodId.Value);
            if(deliveryMethod == null) return null!;

            shippingPrice = deliveryMethod.Price;
        }

        foreach (var cartItem in cart.Items)
        {
            var productItem = await unitOfWork.Repository<Core.Entities.Product>().GetByIdAsync(cartItem.ProductId);
            if (productItem == null) return null!;

            if (cartItem.Price != productItem.Price) 
            {
                cartItem.Price = productItem.Price;
            }
        }

        var paymentService = new PaymentIntentService();
        PaymentIntent? intent = null;

        if (string.IsNullOrEmpty(cart.PaymentIntentId))
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long) cart.Items.Sum(x => x.Quantity * (x.Price * 100)) + (long)shippingPrice * 100,
                Currency = "brl",
                PaymentMethodTypes = ["card"]
            };

            intent = await paymentService.CreateAsync(options);
            cart.PaymentIntentId = intent.Id;
            cart.ClientSecret = intent.ClientSecret;
        }
        else 
        {
            var options = new PaymentIntentUpdateOptions { Amount = (long)cart.Items.Sum(x => x.Quantity * (x.Price * 100) + (long)shippingPrice * 100) };

            intent = await paymentService.UpdateAsync(cart.PaymentIntentId, options);
        }

        await cartService.SetCartAsync(cart);

        return cart;
    }

    public async Task<string> RefundPayment(string paymentIntentId)
    {
        var refundOptions = new RefundCreateOptions{ PaymentIntent = paymentIntentId };

        var refundService = new RefundService();
        var result = await refundService.CreateAsync(refundOptions);

        return result.Status;
    }
}
