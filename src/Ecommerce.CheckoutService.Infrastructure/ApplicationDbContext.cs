using Ecommerce.CheckoutService.Application;
using Ecommerce.CheckoutService.Domain.Entities;
using Ecommerce.CheckoutService.Infrastructure.Errors;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.CheckoutService.Infrastructure;

public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
{
    public DbSet<Order> Orders { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options){ }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public async Task<Result> CommitAsync(CancellationToken cancellationToken)
    {
        try
        {
            await base.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException dbe)
        {
            //map error codes to errors
            return Result.Fail(new DatabaseError().CausedBy(dbe))
                .Log<ApplicationDbContext>("Error while saving context.");
        }    
        catch (Exception e)
        {
            return Result.Fail(new DatabaseError().CausedBy(e))
                .Log<ApplicationDbContext>("Error while saving context.");
        }
        return Result.Ok();
    }
}