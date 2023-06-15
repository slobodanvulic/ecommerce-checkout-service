using Ecommerce.CheckoutService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.CheckoutService.Infrastructure.DbConfiguration;

internal class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.HasOne(o => o.Customer)
            .WithOne()
            .HasForeignKey<Order>("CustomerId")
            .IsRequired();

        builder.HasMany(o => o.OrderItems)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(o => o.Status)
            .HasConversion(
                o => o.ToString(),
                value => Enum.Parse<OrderStatus>(value));
    }
}