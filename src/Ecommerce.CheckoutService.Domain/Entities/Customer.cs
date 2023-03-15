using Ecommerce.CheckoutService.Domain.ValueObjects;

namespace Ecommerce.CheckoutService.Domain.Entities;

public sealed class Customer : Entity
{
    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public Address ShippingAddress { get; private set; }

    public Customer(Guid id, Name name, Email email, Address shippingAddress) : base(id)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));
        ArgumentNullException.ThrowIfNull(email, nameof(email));
        ArgumentNullException.ThrowIfNull(shippingAddress, nameof(shippingAddress));

        Name = name;
        Email = email;
        ShippingAddress = shippingAddress;
    }
}
