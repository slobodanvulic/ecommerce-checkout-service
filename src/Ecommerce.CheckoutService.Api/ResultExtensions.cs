using Ecommerce.CheckoutService.Application.Errors;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ecommerce.CheckoutService.Api;

public static class ResultExtensions
{
    public static ObjectResult MapErrorsToResponse(this Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException("Result is success.");
        }

        if (result.Errors.Any(x => x.HasMetadataKey(ErrorType.Internal)))
        {
            return CreateObjectResult(
                (int)HttpStatusCode.InternalServerError,
                "Server Error",
                "Server Error",
                "An internal server error has occured.");
        }

        //Map other error types

        return CreateObjectResult(
                (int)HttpStatusCode.BadRequest,
                "Client Error",
                "Bad Request",
                string.Join(Environment.NewLine, result.Errors.Select(e => e.Message)));
    }

    public static ObjectResult MapErrorsToResponse<T>(this Result<T> result)
        => result.ToResult().MapErrorsToResponse();

    private static ObjectResult CreateObjectResult(int status, string type, string title, string detail) 
        => new ObjectResult(new ProblemDetails()
        {
            Status = status,
            Type = type,
            Title = title,
            Detail = detail
        })
        {
            StatusCode = status
        };

}
