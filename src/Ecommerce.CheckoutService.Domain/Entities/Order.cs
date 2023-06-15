namespace Ecommerce.CheckoutService.Domain.Entities;

/// <summary>
/// This is an aggregate root which maintains boundary consistency
/// </summary>
public sealed class Order : Entity, IAggregateRoot
{
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset ModifiedAt { get; private set; }
    public Customer Customer { get; private set; }
    public OrderStatus Status { get; private set; }
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.ToList();
    public decimal TotalAmount => _orderItems.Sum(x => x.TotalAmount);

    private readonly List<OrderItem> _orderItems= new();

    private Order(){}

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
    /// Since Order is an aggregate root, this is an only way to add new items to the order.
    /// Any validation and business logic is done by aggregate root.
    /// </summary>
    public void AddOrderItem(Guid productId, int quantity, decimal productPrice, decimal discount)
    {
        var existingOrderForProduct = _orderItems.SingleOrDefault(o => o.ProductId == productId);

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
                    Guid.Empty,
                    productId,
                    quantity,
                    productPrice,
                    discount));
        }
        Status= OrderStatus.Ready;
        ModifiedAt= DateTimeOffset.UtcNow;
    }

    public bool RemoveOrderItem(Guid orderItemId)
    {
        var orderToRemove = _orderItems.SingleOrDefault(oi => oi.Id == orderItemId);

        if (orderToRemove is null)
        {
            return false;
        }

        return _orderItems.Remove(orderToRemove);
    } 

}
