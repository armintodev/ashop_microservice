using EventBus.Messages.Events;

namespace Basket.Entities;

public class BasketCheckout
{
    public string UserName { get; set; }
    public decimal TotalPrice { get; set; }

    // BillingAddress
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string AddressLine { get; set; }
    public string Country { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }

    // Payment
    public string CardName { get; set; }
    public string CardNumber { get; set; }
    public string Expiration { get; set; }
    public string CVV { get; set; }
    public int PaymentMethod { get; set; }

    public static BasketCheckoutEvent FromModel
        (BasketCheckout basketCheckout) =>
        new(
            basketCheckout.UserName,
            basketCheckout.TotalPrice,
            basketCheckout.FirstName,
            basketCheckout.LastName,
            basketCheckout.EmailAddress,
            basketCheckout.AddressLine,
            basketCheckout.Country,
            basketCheckout.State,
            basketCheckout.ZipCode,
            basketCheckout.CardName,
            basketCheckout.CardNumber,
            basketCheckout.Expiration,
            basketCheckout.CVV,
            basketCheckout.PaymentMethod
        );
}