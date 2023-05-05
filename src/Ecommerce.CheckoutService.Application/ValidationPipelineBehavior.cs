using Ecommerce.CheckoutService.Application.Errors;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Ecommerce.CheckoutService.Application;

public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : ResultBase, new()
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> valiators)
    {
        _validators = valiators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validationErrors = GetValidationErrors(request);
        
        if(!validationErrors.Any())
        {
            return await next();
        }

        var result = new TResponse();
        foreach(var error in validationErrors)
        {
            result.Reasons.Add(error);
        }

        return result;
    }

    private IEnumerable<ValidationError> GetValidationErrors(TRequest request)
    {
        var validationFailures = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validatorResult => validatorResult.Errors)
            .Where(validationFailure => validationFailure is not null);

        foreach(var validationFailure in validationFailures)
        {
            yield return new ValidationError(validationFailure.ErrorMessage);
        }
    }
}
