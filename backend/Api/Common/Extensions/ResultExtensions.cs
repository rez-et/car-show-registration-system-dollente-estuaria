namespace Api.Common.Extensions;

using Domain.Common;
using Microsoft.AspNetCore.Mvc;

public static class ResultExtensions
{
    public static IResult ToProblemDetails<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException("Cannot convert success result to ProblemDetails.");
        }

        return CreateProblemResult(result.Error!);
    }

    public static IResult ToProblemDetails(
        this Result result,
        IDictionary<string, string[]>? errors = null)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException("Cannot convert success result to ProblemDetails.");
        }

        return CreateProblemResult(result.Error!, errors);
    }

    private static IResult CreateProblemResult(
        Error error,
        IDictionary<string, string[]>? errors = null)
    {
        var problemDetails = new ProblemDetails
        {
            Title = error.Code,
            Detail = error.Message,
            Status = GetStatusCode(error.Code)
        };

        if (errors is not null)
        {
            problemDetails.Extensions["errors"] = errors;
        }

        return Results.Problem(problemDetails);
    }

    private static int GetStatusCode(string errorCode) => errorCode switch
    {
        string code when code.Contains("NotFound") => 404,
        string code when code.Contains("Validation") => 400,
        string code when code.Contains("Conflict") => 409,
        _ => 500
    };
}
