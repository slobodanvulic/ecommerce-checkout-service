namespace Ecommerce.CheckoutService.Domain.Entities;

public sealed class OrderItem : Entity
{
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public decimal ProductPrice { get; private set; }
    public decimal Discount { get; private set; }
    public decimal TotalAmount => ProductPrice * Quantity - ProductPrice * Quantity * Discount;

    public OrderItem(Guid id, Guid productId, int quantity, decimal productPrice, decimal discount) : base(id)
    {
        //validation

        ProductId = productId;
        Quantity = quantity;
        ProductPrice = productPrice;
        Discount = discount;
    }

    public void SetNewDiscount(decimal discount)
    {
        //validation

        Discount = discount;
    }

    public void IncreaseQuantity(int quantity)
    {
        Quantity += quantity;
    }
}
