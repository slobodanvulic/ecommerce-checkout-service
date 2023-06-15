using Ecommerce.CheckoutService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.CheckoutService.Infrastructure.DbConfiguration;

internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);

        builder.OwnsOne(c => c.Email, emailBuilder =>
        {
            emailBuilder.Property(e => e.Address).HasMaxLength(320);
        });

        builder.OwnsOne(c => c.Name);

        builder.OwnsOne(c => c.ShippingAddress);

        builder.HasOne<Order>()
            .WithOne(o => o.Customer)
            .HasForeignKey<Customer>(c => c.OrderId);
    }
}