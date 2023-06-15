using FluentResults;

namespace Ecommerce.CheckoutService.Application;

public interface IUnitOfWork
{
    public Task<Result> CommitAsync(CancellationToken cancellationToken);

}