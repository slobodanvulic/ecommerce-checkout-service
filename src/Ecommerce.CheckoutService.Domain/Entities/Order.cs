namespace Ecommerce.CheckoutService.Domain.Entities;

/// <summary>
/// This is an aggregate root which maintains consistancy boundary
/// </summary>
public sealed class Order : Entity, IAggregateRoot
{
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset ModifiedAt { get; private set; }
    public Customer Customer { get; private set; }
    public OrderStatus Status { get; private set; }
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public decimal TotalAmount => OrderItems.Sum(x => x.TotalAmount);

    private readonly List<OrderItem> _orderItems= new();

    public static Order NewDraft(Guid id, Customer customer)
    {
        return new Order(id, customer);
    }

    private Order(Guid id, Customer customer): base(id)
    {
        ArgumentNullException.ThrowIfNull(customer, nameof(customer));
        
        CreatedAt = DateTimeOffset.UtcNow;
        ModifiedAt = CreatedAt;
        Customer = customer;
        Status = OrderStatus.Draft;
    }

    /// <summary>
    /// This is only here to create dummy order from a fake repository, will be deleted
    /// </summary>
    public Order(Guid id, Customer customer, List<OrderItem> orderItems) : base(id)
    {
        ArgumentNullException.ThrowIfNull(customer, nameof(customer));
        ArgumentNullException.ThrowIfNull(orderItems, nameof(orderItems));

        CreatedAt = DateTimeOffset.UtcNow;
        ModifiedAt = CreatedAt;
        Customer = customer;
        Status = OrderStatus.Ready;
        _orderItems = orderItems;
    }

    /// <summary>
    /// Since Order is an aggregat root, this is an only way to add new items to the order.
    /// Any validation and business logic is done by aggregate root.
    /// </summary>
    public void AddOrderItem(Guid productId, int quantity, decimal productPrice, decimal discount)
    {
        var existingOrderForProduct = _orderItems.Where(o => o.ProductId == productId).SingleOrDefault();

        if (existingOrderForProduct is not null)
        {
            if(discount > existingOrderForProduct.Discount)
            {
                existingOrderForProduct.SetNewDiscount(discount);
            }
            existingOrderForProduct.IncreaseQuantity(quantity);
        }
        else
        {
            // the product does not exist in the order, create a new and add to the order ites

            _orderItems.Add(
                new OrderItem(
                    Guid.NewGuid(),
                    productId,
                    quantity,
                    productPrice,
                    discount));
        }
        Status= OrderStatus.Ready;
    }
}
