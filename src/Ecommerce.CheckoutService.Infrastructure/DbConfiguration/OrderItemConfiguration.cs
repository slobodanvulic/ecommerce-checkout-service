using Ecommerce.CheckoutService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.CheckoutService.Infrastructure.DbConfiguration;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.Discount).HasColumnType("decimal(18,2)");
        builder.Property(i => i.ProductPrice).HasColumnType("decimal(18,2)");

        builder.HasOne<Order>()
            .WithMany(o => o.OrderItems)
            .HasForeignKey("OrderId")
            .IsRequired();
    }
}