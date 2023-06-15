using Ecommerce.CheckoutService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.CheckoutService.Infrastructure;

public interface IApplicationDbContext
{
    public DbSet<Order> Orders { get; set; }
}